using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;


namespace rulesencyclopediabackend.DAL
{
    public class TOCDAO
    {
        ConvertToDTO DTOConverter = new ConvertToDTO();
        DALExceptionHandling exHandler = new DALExceptionHandling();
        internal List<TOCDTO> getTOCList(int gameID)
        {
            List<TOC> TOCList = null;
            List<TOCDTO> tocDTOs = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    TOCList = context.TOC.Where(element => element.Games == gameID).ToList();
                    tocDTOs = new List<TOCDTO>();
                    foreach (TOC toc in TOCList)
                    {
                        TOCDTO tocDTO = (TOCDTO)DTOConverter.Converter(new TOCDTO(), toc);
                        tocDTOs.Add(tocDTO);
                    }
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something happened while fetching the toclist");
            }
            finally
            {
                context.Dispose();
            }

            return tocDTOs;
        }

        internal TOCDTO getTOC(int ID)
        {
            TOC toc = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    toc = context.TOC.Single(element => element.Id == ID);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting toc");
            }
            finally
            {
                context.Dispose();
            }
            TOCDTO tocDTO = (TOCDTO)DTOConverter.Converter(new TOCDTO(), toc);
            return tocDTO;
        }

        internal int postTOC(TOC toc)
        {
            rulesencyclopediaDBEntities1 context = null;
            TOC result = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    result = context.TOC.Add(toc);
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when creating new toc");
            }
            finally
            {
                context.Dispose();
            }
            return result.Id;
        }

        internal int editTOC(TOC alteredTOC)
        {   
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            TOC toc = null;
            try
            {
                toc = context.TOC.First(a => a.Id == alteredTOC.Id);
                toc.Text = alteredTOC.Text;
                toc.Revision = alteredTOC.Revision;
                toc.Editor = alteredTOC.Editor;
                context.SaveChanges();
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when editing toc");
            }
            finally
            {
                context.Dispose();
            }
            return toc.Id;
        }

        internal int deleteTOC(int ID)
        {
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            TOC toc = null;
            try
            {
                toc = new TOC { Id = ID };
                context.TOC.Attach(toc);
                context.TOC.Remove(toc);
                context.SaveChanges();
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong while deleting the TOC");
            }
            finally
            {
                context.Dispose();
            }
            return toc.Id;
        }
    }
}