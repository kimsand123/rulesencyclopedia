using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Core;
using System.Linq;
using System.Web;

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

        internal void postTOC(TOC toc)
        {
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    TOC result = context.TOC.Add(toc);
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
        }

        internal void editTOC(int ID, TOC alteredTOC)
        {
            {
                rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
                try
                {
                    var toc = context.TOC.First(a => a.Id == ID);
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
            }
        }

        internal void deleteTOC(int ID)
        {
            rulesencyclopediaDBEntities1 context = new rulesencyclopediaDBEntities1();
            try
            {
                var toc = new TOC { Id = ID };
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
        }
    }
}