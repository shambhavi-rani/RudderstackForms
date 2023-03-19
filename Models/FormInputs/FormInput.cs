using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RudderstackForms.Models.FormInputs
{
    [BsonDiscriminator(RootClass = true)]
    [BsonKnownTypes(typeof(FormTextInput), typeof(FormRadioInput), typeof(FormCheckboxInput))]
    [JsonDerivedType(typeof(FormTextInput))]
    [JsonDerivedType(typeof(FormRadioInput))]
    [JsonDerivedType(typeof(FormCheckboxInput))]
    public abstract class FormInput
    {

        //TODO: add password input type too

        /// <summary>
        /// Id fo object
        /// </summary>
        /// 
        /// TODO: update InputType to go as string/enum required by frontend
        /// TODO: Add support for other input types
        [Required]
        public InputType Type { get; set; }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public string Label { get; set; }

        /// <summary>
        /// Id fo object
        /// </summary>
        [Required]
        public bool Required { get; set; }

        protected FormInput(InputType type, string label, bool required)
        {
            Type = type;
            Label = label;
            Required = required;
        }
    }
}
