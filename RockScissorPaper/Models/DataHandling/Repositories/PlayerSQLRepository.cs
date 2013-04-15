﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RockScissorPaper.Models.DataHandling
{
    public class PlayerSQLRepository : IPlayerRepository
    {
        IDatabaseConnector _dataAccess;

        public PlayerSQLRepository(IDatabaseConnector dataAccess)
        {
            _dataAccess = dataAccess;
        }

        public Player RetrievePlayer(int id)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("PlayerIdInput", id));
            PlayerMapper mapper = new PlayerMapper();
            _dataAccess.Get("Proc_Select_PlayerById", mapper, parameters);
            return mapper.Result as Player;
        }

        public int CreatePlayer(string playerName, string ipAddress)
        {
            List<StoreProceedureParameter> parameters = new List<StoreProceedureParameter>();
            parameters.Add(new StoreProceedureParameter("PlayerNameInput", playerName));
            parameters.Add(new StoreProceedureParameter("IpAddressInput", ipAddress));
            return Convert.ToInt32(_dataAccess.GetScalar("Proc_Create_NewPlayer", parameters));
        }
    }
}