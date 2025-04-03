using Microsoft.AspNetCore.Mvc;
using ArteApi.Models;
using Oracle.ManagedDataAccess.Client;
using System.Collections.ObjectModel;
using System.Data;

namespace ArteApi.Controllers
{
    [ApiController]
    [Route("api/paint")]
    public class PaintController : ControllerBase
    {
        [HttpGet("GetAPaint")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetAPaint(Int16 ID )
        {
            //string connectionString = "User Id=system;Password=emiyas17;Data Source=localhost:1521/XE";

            var PaintList = new Collection<Paint>();
            string queryString = string.Format("SELECT ID_, NAME_,TYPE_, DESCRIPCION FROM SYSTEM.PAINT WHERE ID_ARTIST={0} AND DELETE_=0", ID);
            using (OracleConnection connection = new OracleConnection(UI.ConnectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                await connection.OpenAsync();
                OracleDataReader reader = await command.ExecuteReaderAsync();
                try
                {
                    while (await reader.ReadAsync())
                    {
                        var Paint = new Paint
                        {
                            ID = reader.GetInt16(0),
                            Name = reader.GetString(1),
                            Type = reader.GetString(2),
                            Description = reader.GetString(3)
                        };


                        PaintList.Add(Paint);
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


            return PaintList.Count >0? Ok(PaintList): NotFound();
        }


        [HttpGet("GetPaint")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> GetPaint(Int16 ID)
        {
            //string connectionString = "User Id=system;Password=emiyas17;Data Source=localhost:1521/XE";

            var Paint = new Paint();
            string queryString = string.Format("SELECT ID_, NAME_,TYPE_, DESCRIPCION FROM SYSTEM.PAINT WHERE ID_={0} AND DELETE_=0", ID);
            using (OracleConnection connection = new OracleConnection(UI.ConnectionString))
            {
                OracleCommand command = new OracleCommand(queryString, connection);
                await connection.OpenAsync();
                OracleDataReader reader = await command.ExecuteReaderAsync();
                try
                {
                    while (await reader.ReadAsync())
                    {
                        Paint.ID = reader.GetInt16(0);
                        Paint.Name = reader.GetString(1);
                        Paint.Type = reader.GetString(2);
                        Paint.Description = reader.GetString(3);
                        

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


            return Paint.ID == 0 ? NotFound(): Ok(Paint);
        }

        [HttpDelete("DeletePaint")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeletePaint(Int16 ID)
        {

            var result = 0;
            using (OracleConnection connection = new OracleConnection(UI.ConnectionString))
            {
                await connection.OpenAsync();
                OracleCommand cmd = new OracleCommand("SP_DELETE_PAINT", connection);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new OracleParameter("@ID_IN", ID));


                try
                {
                    result = await cmd.ExecuteNonQueryAsync();
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


            return Ok();
        }
    }
}
