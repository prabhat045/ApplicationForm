using ApplicationForm.Core.Model;

namespace ApplicationForm.Manager
{
    public interface IResponseManager
    {
        Task SubmitApplication(Response response);
    }
}
