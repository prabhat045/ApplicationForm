using ApplicationForm.Core.Dto;
using ApplicationForm.Core.Enum;
using ApplicationForm.Core.Model;
using ApplicationForm.Helper;
using ApplicationForm.Manager;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ApplicationFormTests.Helper
{
    public class ResponseHelperTests
    {
        private readonly Mock<IResponseManager> _responseManagerMock;
        private readonly Mock<IProgramApplicationHelper> _programApplicationHelperMock;
        private readonly ResponseHelper _responseHelper;

        public ResponseHelperTests()
        {
            _responseManagerMock = new Mock<IResponseManager>();
            _programApplicationHelperMock = new Mock<IProgramApplicationHelper>();
            _responseHelper = new ResponseHelper(_responseManagerMock.Object, _programApplicationHelperMock.Object);
        }

        [Fact]
        public async Task SubmitResponse_ShouldSubmitResponse_WhenFormExists()
        {
            // Arrange
            var responseDto = new ResponseDto
            {
                programApplicationId = Guid.NewGuid().ToString(),
                FirstName = "TEST",
                LastName = "TEST",
                Email = "TEST@example.com",
                PhoneNumber = 1234567890,
                Nationality = "American",
                CurrentResidence = "New York",
                IdNumber = 12345,
                DateOfBirth = "2000-01-01",
                Gender = "Male",
                Answers = new List<ResponseAnswerDto>
            {
                new ResponseAnswerDto { Label = "Q1", Answer = new List<string>{ "Answer1"
                } }
            }
            };

            var form = new ProgramApplicationDto
            {
                FirstName = new PersonalInfoBaseDto { IsRequired = true },
                LastName = new PersonalInfoBaseDto { IsRequired = true },
                Email = new PersonalInfoBaseDto { IsRequired = true },
                PhoneNumber = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                Nationality = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                CurrentResidence = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                IdNumber = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                DateOfBirth = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                Gender = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                Title = "Test Title",
                Description = "Test Description",
                Questions = new List<QuestionDto>
            {
                new QuestionDto { Label = "Q1", Type = QuestionType.Date, Options = new List<string>() }
            }
            };

            _programApplicationHelperMock.Setup(m => m.GetProgramApplicationById(responseDto.programApplicationId)).ReturnsAsync(form);
            _responseManagerMock.Setup(m => m.SubmitApplication(It.IsAny<Response>())).Returns(Task.CompletedTask);

            await _responseHelper.SubmitResponse(responseDto);

            _programApplicationHelperMock.Verify(m => m.GetProgramApplicationById(responseDto.programApplicationId), Times.Once);
            _responseManagerMock.Verify(m => m.SubmitApplication(It.IsAny<Response>()), Times.Once);
        }

        [Fact]
        public async Task SubmitResponse_ShouldThrowValidationException_WhenFormNotFound()
        {
            var responseDto = new ResponseDto
            {
                programApplicationId = Guid.NewGuid().ToString()
            };

            _programApplicationHelperMock.Setup(m => m.GetProgramApplicationById(responseDto.programApplicationId)).ReturnsAsync((ProgramApplicationDto)null);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _responseHelper.SubmitResponse(responseDto));
            Assert.Equal("Form Not found", exception.Message);

            _programApplicationHelperMock.Verify(m => m.GetProgramApplicationById(responseDto.programApplicationId), Times.Once);
            _responseManagerMock.Verify(m => m.SubmitApplication(It.IsAny<Response>()), Times.Never);
        }

        [Fact]
        public async Task SubmitResponse_ShouldThrowValidationException_WhenRequiredFieldIsMissing()
        {
            var responseDto = new ResponseDto
            {
                programApplicationId = Guid.NewGuid().ToString(),
                FirstName = null,
                LastName = "TEST",
                Email = "TEST@example.com"
            };

            var form = new ProgramApplicationDto
            {
                FirstName = new PersonalInfoBaseDto { IsRequired = true },
                LastName = new PersonalInfoBaseDto { IsRequired = true },
                Email = new PersonalInfoBaseDto { IsRequired = true },
                PhoneNumber = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                Nationality = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                CurrentResidence = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                IdNumber = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                DateOfBirth = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                Gender = new PersonalInfoBaseDto { Hide = false, IsInternal = true },
                Title = "Test Title",
                Description = "Test Description",
                Questions = new List<QuestionDto>
            {
                new QuestionDto { Label = "Q1", Type = QuestionType.Paragraph, Options = new List<string>() }
            }
            };

            _programApplicationHelperMock.Setup(m => m.GetProgramApplicationById(responseDto.programApplicationId)).ReturnsAsync(form);

            var exception = await Assert.ThrowsAsync<ValidationException>(() => _responseHelper.SubmitResponse(responseDto));
            Assert.Equal("FirstName should not be null or empty.", exception.Message);

            _programApplicationHelperMock.Verify(m => m.GetProgramApplicationById(responseDto.programApplicationId), Times.Once);
            _responseManagerMock.Verify(m => m.SubmitApplication(It.IsAny<Response>()), Times.Never);
        }
    }
}
