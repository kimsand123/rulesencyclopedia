using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Linq;
using System.Reflection.Emit;

namespace rulesencyclopediabackend.Controllers
{
    internal class EntryDAO
    {
        ConvertToDTO DTOConverter = new ConvertToDTO();
        public EntryDAO()
        {

        }
        ExceptionHandling exHandler = new ExceptionHandling();
        internal List<EntryDTO> getEntriesForToc(int TOCId)
        {
            List<Entry> entryList = null;
            rulesencyclopediaDBEntities1 context = null;
            EntryDTO entryDTO = null;
            List<EntryDTO> entryDTOs = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    entryList = context.Entry.Where(element => element.TOC == TOCId).ToList();
                    entryDTOs = new List<EntryDTO>();
                    foreach (Entry entry in entryList)
                    {
                        entryDTO = (EntryDTO)DTOConverter.Converter(new EntryDTO(), entry);
                        entryDTOs.Add(entryDTO);
                    }
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
            return entryDTOs;
        }

        internal EntryDTO getEntry(int ID)
        {
            Entry entry = null;
            EntryDTO entryDTO = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    entry = context.Entry.Single(element => element.Id == ID);
                    entryDTO = (EntryDTO)DTOConverter.Converter(new EntryDTO(), entry);
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
            return entryDTO;
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
                entry.Revision = alteredEntry.Revision;
                entry.Editor = alteredEntry.Editor;               
                context.SaveChanges();
            }
            context.Dispose();
        }
    }
}