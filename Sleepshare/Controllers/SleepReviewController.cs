﻿using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Services;
using DataAccessLayer;
using DataAccessLayer.Repositorys;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PresentationLayer.Helpers;
using PresentationLayer.Models;
using Sleepshare.Models;

public class SleepReviewController : Controller
{
    private readonly SleepReviewService _sleepReviewService;
    private readonly FollowerService _followerService;

    // Gebruik Dependency Injection voor de services
    public SleepReviewController(SleepReviewService sleepReviewService, FollowerService followerService)
    {
        _sleepReviewService = sleepReviewService;
        _followerService = followerService;
    }

    public IActionResult SleepFeed()
    {
        var userId = HttpContext.Session.GetInt32("UserId");
        if (userId == null)
        {
            return RedirectToAction("Index", "Login");
        }

        var followedUserIds = _followerService.GetFollowedUserIds(userId.Value);
        followedUserIds.Add(userId.Value); // Voeg de eigen userId toe aan de lijst

        var sleepReviewDTOs = _sleepReviewService.GetSleepReviewsByFollowedUsers(followedUserIds);

        var sleepReviews = sleepReviewDTOs
            .Select(SleepReviewMapper.ToModel)
            .ToList();

        return View(sleepReviews);
    }

    public IActionResult EditReview(int id)
    {
        var sleepReviewDTO = _sleepReviewService.GetAllSleepReviews()
            .FirstOrDefault(s => s.Id == id); 

        if (sleepReviewDTO == null)
        {
            return NotFound();
        }

        var sleepReview = SleepReviewMapper.ToModel(sleepReviewDTO);

        return View(sleepReview); 
    }

    [HttpPost]
    public IActionResult EditReview(SleepReview sleepReview)
    {
        // Gebruik SleepReviewMapper om het model om te zetten naar een DTO
        var sleepReviewDTO = SleepReviewMapper.ToDTO(sleepReview);

        bool isUpdated = _sleepReviewService.UpdateReview(sleepReviewDTO);

        if (isUpdated)
        {
            return RedirectToAction("Profile", "Login");
        }

        return View(sleepReview);
    }

    [HttpGet]
    public IActionResult AddReview()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return View("NotLoggedIn");
        }

        return View("AddReview");
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult AddSleepReview(SleepReview sleepReview)
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Index", "Login");
        }
        sleepReview.UserId = userId.Value;

        var sleepReviewDTO = SleepReviewMapper.ToDTO(sleepReview);

        bool isAdded = _sleepReviewService.AddSleepReview(sleepReviewDTO);

        if (isAdded)
        {
            return RedirectToAction("SleepFeed", "SleepReview");
        }
        else
        {
            ModelState.AddModelError("", "An error occurred while adding the sleep review.");
        }

        return View(sleepReview);
    }

    [HttpPost]
    public IActionResult DeleteReview(int id)
    {
        try
        {
            bool isDeleted = _sleepReviewService.DeleteSleepReview(id); 

            if (isDeleted)
            {
                TempData["SuccessMessage"] = "Sleep review deleted successfully.";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed to delete the sleep review.";
            }
        }
        catch (Exception ex)
        {
            TempData["ErrorMessage"] = $"An error occurred: {ex.Message}";
        }

        return RedirectToAction("Profile", "Login"); 
    }

    //
    [HttpGet]
    [Route("Login/Profile")]
    public IActionResult UserSleepReview()
    {
        var userId = HttpContext.Session.GetInt32("UserId");

        if (userId == null)
        {
            return RedirectToAction("Login", "Login");
        }

        var sleepReviewDTOs = _sleepReviewService.GetSleepReviewsByUserId(userId.Value);

        var sleepReviews = sleepReviewDTOs
            .Select(SleepReviewMapper.ToModel)
            .ToList();

        var model = new ProfileViewModel
        {
            Username = HttpContext.Session.GetString("Username"),
            SleepReviews = sleepReviews
        };

        return View("~/Views/Login/Profile.cshtml", model);

    }



}
