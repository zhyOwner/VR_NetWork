using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Servers
{
    public class PlayerManager 
    {

        private static PlayerManager instance;
        public static PlayerManager Instance{
            get{
                if(instance == null){
                    instance = new PlayerManager();
                    return instance;
                }
                return instance;
            }
        }
       
        private Dictionary<string , Player> _players;

        public PlayerManager()
        {
            _players = new Dictionary<string, Player>();
        }
        
        public bool AddPlayer(string id , Player player){
            if(_players.ContainsKey(id)) return false;
            foreach (Player value in _players.Values)
            {
                if(value.port == "Monitor" && player.port == "Monitor")
                    return false;
            }
            _players.Add(id , player);
            return true;
        }

        public void RemovePlayer(string id){
            if (!_players.ContainsKey(id)) return;
            _players.Remove(id);
        }

        public Player GetPlayer(string id){
            if(!_players.ContainsKey(id)) return null;
            return _players[id];
        }

        public string GetPlayer(){
            string players = "";
            foreach (var item in _players.Values)
            {
                players += ("|" +item.Id +"-" +item.port);
            }
            return players;
        }
    }
}

