using ApplicationForm.Core.Dto;
using ApplicationForm.Core.Model;
using ApplicationForm.Manager;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace ApplicationForm.Helper
{
    public class ResponseHelper :IResponseHelper
    {
        private readonly IResponseManager _responseManager;
        private readonly IProgramApplicationHelper _programApplicationHelper;

        public ResponseHelper(IResponseManager responseManager,IProgramApplicationHelper programApplicationHelper)
        {
            _responseManager = responseManager;
            _programApplicationHelper = programApplicationHelper;
        }

        public async Task SubmitResponse(ResponseDto responseDto)
        {
            var form = await _programApplicationHelper.GetProgramApplicationById(responseDto.programApplicationId).ConfigureAwait(false);
            if(form == null)
            {
                throw new ValidationException("Form Not found");
            }
            ValidateAnswers(responseDto, form);

            var response = new Response
            {
                Id = Guid.NewGuid().ToString(),
                programApplicationId = responseDto.programApplicationId,
                FirstName = responseDto.FirstName,
                LastName = responseDto.LastName,
                Email = responseDto.Email,
                PhoneNumber = responseDto.PhoneNumber,
                Nationality = responseDto.Nationality,
                CurrentResidence = responseDto.CurrentResidence,
                IdNumber = responseDto.IdNumber,
                DateOfBirth = responseDto.DateOfBirth != null ? DateOnly.Parse(responseDto.DateOfBirth) : null,
                Gender = responseDto.Gender,
                Answers = responseDto.Answers.Select(a => new ResponseAnswers
                {
                     Label = a.Label,
                     Answer = a.Answer
                }).ToList()
            };

            await _responseManager.SubmitApplication(response).ConfigureAwait(false);
        }


        private void ValidateRequiredField<T>(bool isRequired, T fieldValue, string fieldName)
        {
            if (isRequired && EqualityComparer<T>.Default.Equals(fieldValue, default(T)))
            {
                throw new ValidationException($"{fieldName} should not be null or empty.");
            }
        }

        private void ValidateAnswers(ResponseDto responseDto, ProgramApplicationDto form)
        {
            ValidateRequiredField(form.FirstName.IsRequired, responseDto.FirstName, nameof(responseDto.FirstName));
            ValidateRequiredField(form.LastName.IsRequired, responseDto.LastName, nameof(responseDto.LastName));
            ValidateRequiredField(form.Email.IsRequired, responseDto.Email, nameof(responseDto.Email));
        }

    }
}
