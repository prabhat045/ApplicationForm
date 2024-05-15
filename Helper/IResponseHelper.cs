using ApplicationForm.Core.Dto;

namespace ApplicationForm.Helper
{
    public interface IResponseHelper
    {
        Task SubmitResponse(ResponseDto responseDto);
    }
}
