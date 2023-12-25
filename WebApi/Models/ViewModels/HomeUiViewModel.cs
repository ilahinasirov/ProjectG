using Entities.Concrete;

namespace WebApi.Models.ViewModels
{
    public class HomeUiViewModel : BaseViewModel
    {
        public HomeUiViewModel()
        {
            Requests = Requests ?? new List<Request>();
        }

        public List<Request> Requests { get; set; }
    }
}
