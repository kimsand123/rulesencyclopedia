using rulesencyclopediabackend.Exceptions;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Reflection.Emit;

namespace rulesencyclopediabackend.Controllers
{
    internal class EntryDAO
    {

        public EntryDAO()
        {

        }
        ExceptionHandling exHandler = new ExceptionHandling();
        internal List<Entry> getEntriesForToc(int TOCId)
        {
            List<Entry> entryList = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    entryList = context.Entry.ToList();
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something happened while fetching the entryList");
            }
            finally
            {
                // context.Dispose();
            }
            return entryList;
        }

        internal Entry getEntry(int ID)
        {
            Entry entry = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    entry = context.Entry.Single(element => element.Id == ID);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting the Entry");
            }
            finally
            {
                context.Dispose();
            }
            return entry;
        }

        internal void postEntry(Entry entry)
        {
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    Entry result = context.Entry.Add(entry);
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong when creating new game");
            }
            finally
            {
                context.Dispose();
            }
        }

        internal void deleteEntry(int ID)
        {
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    var entry = new Entry { Id = ID };
                    context.Entry.Attach(entry);
                    context.Entry.Remove(entry);
                    context.SaveChanges();
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "Something went wrong while deleting the user");
            }
            finally
            {
                context.Dispose();
            }
        }

        internal void editEntry(int ID, Entry alteredEntry)
        {
            var context = new rulesencyclopediaDBEntities1();
            {
                var entry = context.Entry.First(a => a.Id == ID);
                entry.ParagraphNumber = alteredEntry.ParagraphNumber;
                entry.Headline = alteredEntry.Headline;
                entry.Text = alteredEntry.Text;
                entry.Revisions = alteredEntry.Revisions;
                entry.Editor = alteredEntry.Editor;               
                context.SaveChanges();
            }
            context.Dispose();
        }
    }
}