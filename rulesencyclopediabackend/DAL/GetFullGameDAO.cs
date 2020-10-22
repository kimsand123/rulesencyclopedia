using rulesencyclopediabackend.Controllers;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rulesencyclopediabackend.DAL
{
    public class GetFullGameDAO
    {
        GameDAO gameDao = new GameDAO();
        TOCDAO tocDao = new TOCDAO();
        EntryDAO entryDao = new EntryDAO();
        FullGameDTO fullGame = new FullGameDTO();
        GameDTO gameDTO;
        ConvertToDTO DTOConverter = new ConvertToDTO();
        List<EntryDTO> entryList = new List<EntryDTO>();
        public FullGameDTO getTheGame(int id)
        {
            fullGame.game = gameDao.getGame(id);
            List<TOCDTO> fullTocList = tocDao.getTOCList(id);
            List<FullTOCDTO> tocList = new List<FullTOCDTO>();
            foreach(TOCDTO toc in fullTocList)
            {
                entryList = entryDao.getEntriesForToc(toc.Id);
                FullTOCDTO fullToc = new FullTOCDTO();
                fullToc.toc = toc;
                fullToc.entryList = entryList;
                tocList.Add(fullToc);
            }
            fullGame.tocList = tocList;            
            return fullGame;
        }
    }
}