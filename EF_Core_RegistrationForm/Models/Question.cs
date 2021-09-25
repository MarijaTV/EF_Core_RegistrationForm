﻿using System.Collections.Generic;

namespace EF_Core_RegistrationForm.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public List<Answer> Answers { get; set; }
    }
}
