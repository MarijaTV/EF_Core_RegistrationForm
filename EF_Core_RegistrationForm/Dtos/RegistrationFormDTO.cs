using System.Collections.Generic;
using EF_Core_RegistrationForm.Models;

namespace EF_Core_RegistrationForm.Dtos
{
    public class RegistrationFormDTO
    {
        public List<QuestionAnswer> QuestionAnswers { get; set; }
        public int RegistrationId { get; set; }
    }
}
