using rulesencyclopediabackend.Data;
using rulesencyclopediabackend.Exceptions;
using rulesencyclopediabackend.Models;
using rulesencyclopediabackend.Tools;
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
        DALExceptionHandling exHandler = new DALExceptionHandling();
        ConvertToDTO DTOConverter = new ConvertToDTO();
        public GameDAO()
        {

        }
        internal List<GameDTO> getGameList()
        {
            List<Game> gameList = null;
            List<GameDTO> gameDTOs = null;
            rulesencyclopediaDBEntities1 context = null;
            
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    //Getting the games list from DB
                    gameList = context.Game.ToList();
                    gameDTOs = new List<GameDTO>();

                    foreach (Game game in gameList)
                    {
                        GameDTO gameDTO = (GameDTO)DTOConverter.Converter(new GameDTO(), game);
                        gameDTOs.Add(gameDTO);
                    }
                }
            } catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something happened while fetching the gamelist");
            }
            finally
            { 
               context.Dispose();
            }
            return gameDTOs;
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

        internal GameDTO getGame(int ID)
        {
            Game game = null;
            GameDTO gameDTO = null;
            rulesencyclopediaDBEntities1 context = null;
            try
            {
                context = new rulesencyclopediaDBEntities1();
                {
                    game = context.Game.Single(element => element.Id == ID);
                    gameDTO = (GameDTO)DTOConverter.Converter(new GameDTO(), game);
                }
            }
            catch (EntityException ex)
            {
                exHandler.exceptionHandlerEntity(ex, "something went wrong when getting game");
            } 
            catch (System.InvalidOperationException ex)
            {
                exHandler.exceptionHandlerInvalidOperation(ex, "Something went wrong. Record not found.");
            }
            finally
            {
                context.Dispose();
            }
            return gameDTO;
        }

    }
}