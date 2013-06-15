
using HilltopDigital.SimpleDAL;
using RockScissorPaper.Domain;
using System;
using System.Collections.Generic;

namespace RockScissorPaper.DAL
{
    /// <summary>
    /// Required Stored Proceedures
    /// Proc_Select_PlayerById
    /// Proc_Create_NewPlayer
    /// </summary>
    public class PlayerSQLRepository : IPlayerRepository
    {
        IDatabaseConnector _dataAccess;

        public PlayerSQLRepository(IDatabaseConnector dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Player GetPlayer(int id)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("PlayerIdInput", id));
            PlayerMapper mapper = new PlayerMapper();
            _dataAccess.Get("Proc_Select_PlayerById", mapper, parameters);
            return mapper.Result as Player;
        }

        public int CreatePlayer(string playerName, string ipAddress)
        {
            List<StoreProcedureParameter> parameters = new List<StoreProcedureParameter>();
            parameters.Add(new StoreProcedureParameter("PlayerNameInput", playerName));
            parameters.Add(new StoreProcedureParameter("IpAddressInput", ipAddress));
            return Convert.ToInt32(_dataAccess.GetScalar("Proc_Create_NewPlayer", parameters));
        }
    }
}