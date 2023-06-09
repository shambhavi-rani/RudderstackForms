﻿using MongoDB.Bson.Serialization.Attributes;
using RudderstackForms.Models.FormInputs;

namespace RudderstackForms.Models
{
    public class FormTemplate
    {
        /// <summary>
        /// Source type of the form template
        /// Will be unique for each form
        /// </summary>
        [BsonId]
        public string Type { get; set; } = null!;

        /// <summary>
        /// Dictionary of fields for the form template
        /// Key is field name
        /// value in object of type FormInput
        /// </summary>
        public Dictionary<string, FormInput> Fields { get; set; } = null!;
    }
}
