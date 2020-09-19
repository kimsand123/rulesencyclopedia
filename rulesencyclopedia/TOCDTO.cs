using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace rulesencyclopedia
{
    public class TOCDTO
    {
        private UserDTO Editor;
        private EntryDTO[] Rules;

        public int ID
        {
            get => default;
            set
            {
            }
        }

        public string Text
        {
            get => default;
            set
            {
            }
        }

        public RevisionDTO Revision
        {
            get => default;
            set
            {
            }
        }
    }
}