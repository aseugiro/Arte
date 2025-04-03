using ArteApi.Models;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Data;

namespace ArteApi.Controllers
{
    [ApiController]
    [Route("api/artist")]
    public class ArtistController : ControllerBase
    {
        [HttpGet("GetArtist")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetArtist()
        {
            //string connectionString = "User Id=system;Password=emiyas17;Data Source=localhost:1521/XE";
          
            var ArtistList = new Collection<Artist>();
            string queryString = "SELECT ID_, NAME_,COUNTRY, DESCRIPCION FROM SYSTEM.ARTIST";
            using (OracleConnection connection = new OracleConnection(UI.ConnectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                await connection.OpenAsync();
                 OracleDataReader  reader = await command.ExecuteReaderAsync();
                try
                {
                    while (await reader.ReadAsync())
                    {
                        var Artist = new Artist
                        {
                            ID = reader.GetInt16(0),
                            Name = reader.GetString(1),
                            Country= reader.GetString(2),
                            Description=reader.GetString(3)
                        };
                       

                        ArtistList.Add(Artist);
                    }
                }catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // always call Close when done reading.
                   await reader.CloseAsync();
                }
            }

           
            return  Ok(ArtistList);
        }


        [HttpGet("GetArtistByID")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetArtistByID(int ID)
        {
            //string connectionString = "User Id=system;Password=emiyas17;Data Source=localhost:1521/XE";

            var Artist = new Artist();
            string queryString = string.Format("SELECT ID_, NAME_, COUNTRY, DESCRIPCION FROM SYSTEM.ARTIST WHERE ID_ = {0}", ID);
            using (OracleConnection connection = new OracleConnection(UI.ConnectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                await connection.OpenAsync();
                OracleDataReader reader = await command.ExecuteReaderAsync();
                try
                {
                    while (await reader.ReadAsync())
                    {
                        Artist.ID = reader.GetInt16(0);
                        Artist.Name = reader.GetString(1);  
                        Artist.Country= reader.GetString(2);
                        Artist.Description=reader.GetString(3);

                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // always call Close when done reading.
                    await reader.CloseAsync();
                }
            }
            
             return Artist.ID==0 ? NotFound() : Ok(Artist);
           
        }

        [HttpPost("PostArtist")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PostArtist(Artist Artist)
        {

            var ArtistList = new Collection<Artist>();
            var result=0;
            using (OracleConnection connection = new OracleConnection(UI.ConnectionString))
            {
                await connection.OpenAsync();
                OracleCommand cmd = new OracleCommand("SP_UPDATE_ARTIST", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add( new OracleParameter("@ID_IN", Artist.ID));
                cmd.Parameters.Add(new OracleParameter("@NAME_IN", Artist.Name));
                cmd.Parameters.Add(new OracleParameter("@COUNTRY_IN", Artist.Country));
                cmd.Parameters.Add(new OracleParameter("@DESCRIPCION_IN", Artist.Description));


                try
                {
                    result=await cmd.ExecuteNonQueryAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    // always call Close when done reading.
                    await connection.CloseAsync();
                }
            }


            return result> 0 ? NotFound() : Ok(Artist);
        }

    }


}


public static class UI
{
    public static string ConnectionString { get; set; } = string.Empty;
}