using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rulesencyclopedia
{
    public class TutorialDTO
    {
        private RevisionDTO Revision;
        private UserDTO Editor;
        private StepDTO[] Steps;

        public int ID
        {
            get => default;
            set
            {
            }
        }

        public String Name
        {
            get => default;
            set
            {
            }
        }
    }
}