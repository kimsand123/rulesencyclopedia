using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rulesencyclopedia
{
    public class RulebookDTO
    {
        private RevisionDTO Revision;
        private UserDTO Editor;
        private TOCDTO[] TOCs;
        private TutorialDTO[] Tutorials;

        public int ID
        {
            get => default;
            set
            {
            }
        }

        public string Game
        {
            get => default;
            set
            {
            }
        }

        public string Company
        {
            get => default;
            set
            {
            }
        }
    }
}