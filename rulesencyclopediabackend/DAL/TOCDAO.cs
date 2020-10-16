using rulesencyclopediabackend.Exceptions;
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
        ExceptionHandling exHandler = new ExceptionHandling();
        internal List<TOC> getTOCList(int gameID)
        {
            List<TOC> TOCList = null;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    TOCList = context.TOC.Where(element => element.Id == gameID).ToList();
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something happened while fetching the gamelist");
            }
            return TOCList;
        }

        internal TOC getTOC(int ID)
        {
            TOC toc = null;
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    toc = context.TOC.Single(element => element.Id == ID);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting game");
            }
            return toc;
        }

        internal void postTOC(TOC toc)
        {
            try
            {
                var context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    TOC result = context.TOC.Add(toc);
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when creating new game");
            }
        }

        internal void editTOC(int ID, TOC alteredTOC)
        {
            var context = new rulesencyclopediaDBEntities1();
            {
                var toc = context.TOC.First(a => a.Id == ID);
                toc.Text = alteredTOC.Text;
                toc.Revisions = alteredTOC.Revisions;
                toc.Editor = alteredTOC.Editor;
                context.SaveChanges();
            }
        }

        internal void deleteTOC(int ID)
        {
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    var toc = new TOC { Id = ID };
                    context.TOC.Attach(toc);
                    context.TOC.Remove(toc);
                    context.SaveChanges();
                }
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