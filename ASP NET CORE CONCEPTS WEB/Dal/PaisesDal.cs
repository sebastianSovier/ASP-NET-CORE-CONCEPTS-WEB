using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using NetCoreConcepts.Models;
using System;
using System.Collections.Generic;

namespace NetCoreConcepts.Dal
{
    public class PaisesDal
    {
        private readonly IConfiguration _config;

        public PaisesDal()
        {
        }
        public PaisesDal(IConfiguration config)
        {
            _config = config;
        }

        public List<PaisesModel> ObtenerPaises()
        {   
            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                List<PaisesModel> listPaises = new List<PaisesModel>();
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = $"select pais_id,nombre_pais,capital,region,poblacion,fecha_registro from Paises order by pais_id;";
                /*cmd.Parameters.Add("?articuloId", MySqlDbType.Int32).Value = articuloId;*/

                
                using (var reader = cmd.ExecuteReader())
                {
                    
                    while (reader.Read())
                    {
                        PaisesModel paises = new PaisesModel();
                        paises.pais_id = Convert.ToInt32(reader["pais_id"]);
                        paises.nombre_pais = reader["nombre_pais"].ToString();
                        paises.capital = reader["capital"].ToString();
                        paises.region = reader["region"].ToString();
                        paises.poblacion = reader["poblacion"].ToString();
                        listPaises.Add(paises);
                       
                    }
                }
                return listPaises;
            }
        }
        public List<CiudadesModel> ObtenerCiudades(string pais_id)
        {
            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                List<CiudadesModel> listaCiudades = new List<CiudadesModel>();
                conexion.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = $"select ciudad_id,pais_id,nombre_ciudad,poblacion,region,fecha_registro,latitud,longitud from Ciudades where pais_id = ?pais_id order by pais_id;";
                cmd.Parameters.Add("?pais_id", MySqlDbType.Int32).Value = pais_id;


                using (var reader = cmd.ExecuteReader())
                {

                    while (reader.Read())
                    {
                        CiudadesModel paises = new CiudadesModel();
                        paises.ciudad_id = Convert.ToInt32(reader["ciudad_id"]);
                        paises.pais_id = Convert.ToInt32(reader["pais_id"]);
                        paises.nombre_ciudad = reader["nombre_ciudad"].ToString();
                        paises.region = reader["region"].ToString();
                        paises.poblacion = reader["poblacion"].ToString();
                        paises.latitud = reader["latitud"].ToString();
                        paises.longitud = reader["longitud"].ToString();
                        listaCiudades.Add(paises);

                    }
                }
                return listaCiudades;
            }
        }

        public void InsertarPaises(PaisesModel paisRequest) {

            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "INSERT INTO `bdPaises`.`Paises` (`nombre_pais`, `capital`, `region`, `poblacion`) VALUES (?nombre_pais, ?capital, ?region, ?poblacion);";

                cmd.Parameters.Add("?nombre_pais", MySqlDbType.VarChar).Value = paisRequest.nombre_pais;
                cmd.Parameters.Add("?capital", MySqlDbType.VarChar).Value = paisRequest.capital;
                cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = paisRequest.region;
                cmd.Parameters.Add("?poblacion", MySqlDbType.VarChar).Value = paisRequest.poblacion;

                cmd.ExecuteNonQuery();
            }
        }
        public void InsertarCiudad(CiudadesModel ciudadRequest)
        {

            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "INSERT INTO `bdPaises`.`Ciudades` (`pais_id`, `nombre_ciudad`, `poblacion`, `region`, `latitud`, `longitud`) VALUES (?pais_id, ?nombre_ciudad, ?poblacion, ?region, ?latitud, ?longitud);";

                cmd.Parameters.Add("?pais_id", MySqlDbType.VarChar).Value = ciudadRequest.pais_id;
                cmd.Parameters.Add("?nombre_ciudad", MySqlDbType.VarChar).Value = ciudadRequest.nombre_ciudad;
                cmd.Parameters.Add("?poblacion", MySqlDbType.VarChar).Value = ciudadRequest.poblacion;
                cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = ciudadRequest.region;
                cmd.Parameters.Add("?latitud", MySqlDbType.VarChar).Value = ciudadRequest.latitud;
                cmd.Parameters.Add("?longitud", MySqlDbType.VarChar).Value = ciudadRequest.longitud;

                cmd.ExecuteNonQuery();
            }
        }

        public void ModificarPais(PaisesModel paisRequest)
        {

            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "UPDATE `bdPaises`.`Paises` set nombre_pais = ?nombre_pais, capital = ?capital, region = ?region, poblacion = ?poblacion where pais_id = ?pais_id";

                cmd.Parameters.Add("?pais_id", MySqlDbType.VarChar).Value = paisRequest.pais_id;
                cmd.Parameters.Add("?nombre_pais", MySqlDbType.VarChar).Value = paisRequest.nombre_pais;
                cmd.Parameters.Add("?capital", MySqlDbType.VarChar).Value = paisRequest.capital;
                cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = paisRequest.region;
                cmd.Parameters.Add("?poblacion", MySqlDbType.VarChar).Value = paisRequest.poblacion;

                cmd.ExecuteNonQuery();
            }
        }
        public void ModificarCiudad(CiudadesModel ciudadRequest)
        {

            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "UPDATE `bdPaises`.`Ciudades` set nombre_ciudad = ?nombre_ciudad, region = ?region, poblacion = ?poblacion, latitud = ?latitud, longitud = ?longitud where ciudad_id = ?ciudad_id";

                cmd.Parameters.Add("?nombre_ciudad", MySqlDbType.VarChar).Value = ciudadRequest.nombre_ciudad;
                cmd.Parameters.Add("?region", MySqlDbType.VarChar).Value = ciudadRequest.region;
                cmd.Parameters.Add("?poblacion", MySqlDbType.VarChar).Value = ciudadRequest.poblacion;
                cmd.Parameters.Add("?ciudad_id", MySqlDbType.VarChar).Value = ciudadRequest.ciudad_id;
                cmd.Parameters.Add("?latitud", MySqlDbType.VarChar).Value = ciudadRequest.latitud;
                cmd.Parameters.Add("?longitud", MySqlDbType.VarChar).Value = ciudadRequest.longitud;

                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarPais(string pais_id)
        {

            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "Delete from `bdPaises`.`Paises` where pais_id = ?pais_id ";

                cmd.Parameters.Add("?pais_id", MySqlDbType.VarChar).Value = pais_id;

                cmd.ExecuteNonQuery();
            }
        }
        public void EliminarCiudad(string ciudad_id)
        {

            using (MySqlConnection conexion = new MySqlConnection(_config.GetValue<string>("Data:ConnectionStrings:DefaultConnection")))
            {
                conexion.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conexion;
                cmd.CommandText = "Delete from `bdPaises`.`Ciudades` where ciudad_id = ?ciudad_id ";

                cmd.Parameters.Add("?ciudad_id", MySqlDbType.VarChar).Value = ciudad_id;

                cmd.ExecuteNonQuery();
            }
        }
    }
}
