

using ApplicationForm.Core.Dto;
using ApplicationForm.Core.Model;
using ApplicationForm.Manager;
using Moq;
using Shouldly;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace ApplicationForm.Helper.Tests
{

    public class ProgramApplicationHelperTests
    {
        private readonly Mock<IProgramApplicationManager> _programApplicationManagerMock;
        private readonly IProgramApplicationHelper _programApplicationHelper;

        public ProgramApplicationHelperTests()
        {
            _programApplicationManagerMock = new Mock<IProgramApplicationManager>();
            _programApplicationHelper = new ProgramApplicationHelper(_programApplicationManagerMock.Object);
        }

        [Fact]
        public async Task CreateProgram_ShouldReturnProgramId_WhenProgramIsCreated()
        {
            var programApplication = new ProgramApplicationDto
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
                new QuestionDto { Label = "Q1", Type = Core.Enum.QuestionType.YesNo, Options = new List<string>{
                "Yes","No"} }
            }
            };

            var programApplicationId = Guid.NewGuid().ToString();
            _programApplicationManagerMock.Setup(m => m.CreateProgram(It.IsAny<ProgramApplication>())).ReturnsAsync(programApplicationId);

            var result = await _programApplicationHelper.CreateProgram(programApplication);

            Assert.Equal(programApplicationId, result);
            _programApplicationManagerMock.Verify(m => m.CreateProgram(It.IsAny<ProgramApplication>()), Times.Once);
        }

        [Fact]
        public async Task CreateProgram_ShouldThrowException_WhenManagerFails()
        {
            var programApplicationDto = new ProgramApplicationDto
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
                new QuestionDto { Label = "Q1", Type = Core.Enum.QuestionType.Date,
                    Options = new List<string>() }
            }
            };

            _programApplicationManagerMock.Setup(m => m.CreateProgram(It.IsAny<ProgramApplication>())).ThrowsAsync(new Exception("Manager failure"));

            await Assert.ThrowsAsync<Exception>(() => _programApplicationHelper.CreateProgram(programApplicationDto));
            _programApplicationManagerMock.Verify(m => m.CreateProgram(It.IsAny<ProgramApplication>()), Times.Once);
        }

        [Fact]
        public async Task UpdateProgram_ShouldReturnProgramId_WhenProgramIsUpdated()
        {
            var programApplicationDto = new ProgramApplicationDto
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
                new QuestionDto { Label = "Q1", Type = Core.Enum.QuestionType.Number, Options = new List<string>() },
                 new QuestionDto { Label = "Q2", Type = Core.Enum.QuestionType.Paragraph, Options = new List<string>() },
            }
            };

            var programId = Guid.NewGuid().ToString();
            _programApplicationManagerMock.Setup(m => m.UpdateProgram(It.IsAny<ProgramApplication>(), programId)).ReturnsAsync(programId);

            var result = await _programApplicationHelper.UpdateProgram(programApplicationDto, programId);

            Assert.Equal(programId, result);
            _programApplicationManagerMock.Verify(m => m.UpdateProgram(It.IsAny<ProgramApplication>(), programId), Times.Once);
        }

        [Fact]
        public async Task GetProgramApplicationById_ShouldReturnProgramApplicationDto_WhenProgramExists()
        {
            // Arrange
            var programId = Guid.NewGuid().ToString();
            var programApplication = new ProgramApplication
            {
                FirstName = new PersonalInfoBase(true),
                LastName = new PersonalInfoBase(true),
                Email = new PersonalInfoBase(true),
                PhoneNumber = new PersonalInfoBase(false, true),
                Nationality = new PersonalInfoBase(false, true),
                CurrentResidence = new PersonalInfoBase(false, true),
                IdNumber = new PersonalInfoBase(false, true),
                DateOfBirth = new PersonalInfoBase(false, true),
                Gender = new PersonalInfoBase(false, true),
                Id = programId,
                Title = "Test Title",
                Description = "Test Description",
                Question = new List<Question>
            {
                new Question { Label = "Q1", Type = Core.Enum.QuestionType.DropDown, Options = new List<string>{
                   "option1","option2"} }
            }
            };

            _programApplicationManagerMock.Setup(m => m.GetProgramApplicationById(programId)).ReturnsAsync(programApplication);

            var result = await _programApplicationHelper.GetProgramApplicationById(programId);

            Assert.NotNull(result);
            Assert.IsType<ProgramApplicationDto>(result);
            Assert.Equal(programApplication.Title, result.Title);
            Assert.Equal(programApplication.Description, result.Description);
            Assert.Equal(programApplication.FirstName.IsRequired, result.FirstName.IsRequired);

            _programApplicationManagerMock.Verify(m => m.GetProgramApplicationById(programId), Times.Once);
        }

        [Fact]
        public async Task GetProgramApplicationById_ShouldThrowValidationException_WhenProgramDoesNotExist()
        {
            // Arrange
            var programId = Guid.NewGuid().ToString();

            _programApplicationManagerMock.Setup(m => m.GetProgramApplicationById(programId)).ReturnsAsync((ProgramApplication)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ValidationException>(() => _programApplicationHelper.GetProgramApplicationById(programId));
            Assert.Equal("Form Not found", exception.Message);

            _programApplicationManagerMock.Verify(m => m.GetProgramApplicationById(programId), Times.Once);
        }

    }
}