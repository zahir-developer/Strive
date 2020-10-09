using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strive.BusinessEntities.ViewModel
{
    public class WhiteLabelViewModel
    {
        public int WhiteLabelId { get; set; }
        public string LogoPath { get; set; }
        public string Base64 { get; set; }
        public string Title { get; set; }
        public int ThemeId { get; set; }
        public string FontFace { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }

    }
}
