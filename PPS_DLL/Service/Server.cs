using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PPS_DLL.Service
{
    public class Server : People , IMobile
    {

        public Square ActualSquare { get; set; }
        public Table ActualTable { get; set; }
        public bool IsAvailable;

        public Server()
        {

        }
        
        /// <summary>
        /// Implémentation People
        /// </summary>
        public override int Id
        {
            get { return Id; }
        }

        public override void Wait(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
        }

        /// <summary>
        /// Implémentation IMobile
        /// </summary>
        
        public void Move(Square newSquare, Table newTable)
        {
            this.ActualTable = newTable;
            this.ActualSquare = newSquare;
        }

        /// <summary>
        /// Methods
        /// </summary>

        public void Serve(Order order) //sert la commande d'une table 
        {
            
        }

        public void ClearTable(Table t) // nettoie la table
        {
            if (t.IsCleaned == false)
            {
                t.IsCleaned = true;
                t.IsDressed = true;
            }
        }
    }
}
