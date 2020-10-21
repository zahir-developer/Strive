using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class ThemeViewModel
    {
        public int ThemeId { get; set; }
        public string ThemeName { get; set; }
        public string FontFace { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public string NavigationColor { get; set; }
        public string BodyColor { get; set; }
        public string DefaultLogoPath { get; set; }
        public string DefaultTitle { get; set; }
        public string Comments { get; set; }
    }
}
