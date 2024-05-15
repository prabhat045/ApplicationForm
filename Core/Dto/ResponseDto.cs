﻿using ApplicationForm.Core.Model;

namespace ApplicationForm.Core.Dto
{
    public class ResponseDto :ResponsePersonalInfoDto
    {
        public string programApplicationId { get; set; }

        public List<ResponseAnswerDto> Answers { get; set; }
    }
}
