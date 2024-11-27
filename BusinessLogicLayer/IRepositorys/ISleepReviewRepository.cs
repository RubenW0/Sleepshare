


using BusinessLogicLayer.DTOs;
using BusinessLogicLayer.Entitys;

namespace BusinessLogicLayer.IRepositorys
{
    public interface ISleepReviewRepository
    {
        List<SleepReviewDTO> GetSleepReviews();
        bool UpdateSleepReview(SleepReviewDTO review);
        bool AddSleepReview(SleepReviewDTO review);
    }
}
