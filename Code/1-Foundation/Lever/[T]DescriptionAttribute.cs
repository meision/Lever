using System;

namespace Meision
{
    public class DescriptionAttribute : Attribute
    {
        private string _description;
        public string Description
        {
            get
            {
                return this._description;
            }
        }

        public DescriptionAttribute(string description)
        {
            this._description = description;
        }
    }
}
