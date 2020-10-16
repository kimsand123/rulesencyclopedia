using rulesencyclopediabackend.Data;
using rulesencyclopediabackend.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.Razor.Text;

namespace rulesencyclopediabackend.DAL
{
    public class GameDAO
    {
        ExceptionHandling exHandler = new ExceptionHandling();
        public GameDAO()
        {

        }

        public List<Game> getGameList()
        {
            List<Game> gameList = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    gameList = context.Game.ToList();
                }
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something happened while fetching the gamelist");
            }
            finally
            {
               // context.Dispose();
            }
            return gameList;
        }

        internal void postGame(Game game)
        {
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    //getting back the key for the created user.
                    Game result = context.Game.Add(game);
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

        internal void deleteGame(int ID)
        {
            rulesencyclopediaDBEntities1 context=null;
            try
            { 
                context = new rulesencyclopediaDBEntities1();
                {
                    var game = new Game { Id = ID };
                    context.Game.Attach(game);
                    context.Game.Remove(game);
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

        internal void editGame(int ID, Game alteredGame)
        {
            var context = new rulesencyclopediaDBEntities1();
            {
                var game = context.Game.First(a => a.Id == ID);
                game.Name = alteredGame.Name;
                game.Company = alteredGame.Company;
                game.TOC = alteredGame.TOC;
                game.Revision = alteredGame.Revision;
                game.Editor = alteredGame.Editor;
                context.SaveChanges();
            }
            context.Dispose();
        }

        internal Game getGame(int ID)
        {
            Game game = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    game = context.Game.Single(element => element.Id == ID);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting game");
            } 
            finally
            {
                context.Dispose();
            }
            return game;
        }

    }
}