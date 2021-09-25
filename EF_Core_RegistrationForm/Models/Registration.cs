using System.Collections.Generic;

namespace EF_Core_RegistrationForm.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public List<QuestionAnswer> QuestionAnswers { get; set; }
    }
}
