using GMS_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GMS_Business
{
    public class clsEquipment
    {
        enum enMode { AddNew, Update}
        enMode Mode = enMode.AddNew;

        public enum enCondition {Unbroken, Broken}
        public enCondition Condition = enCondition.Unbroken;

        public int EquipmentID {  get; set; }
        public string Name { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime LastMaintenanceDate {  get; set; }
        public string EquipmentImage {  get; set; }
        public int AddedByUserID {  get; set; }
        public clsUser UserInfo;

        public clsEquipment() 
        {
            Mode = enMode.AddNew;
            EquipmentID = -1; Name = ""; Condition = (byte)enCondition.Unbroken;
            PurchaseDate = DateTime.Now; LastMaintenanceDate = DateTime.MaxValue;
            EquipmentImage = ""; AddedByUserID = -1;
        }

        public clsEquipment(int equipmentID, string name, enCondition condition, DateTime purchaseDate
            , DateTime lastMaintenanceDate, string equipemtImage, int addedByUserID)
        {
            Mode = enMode.Update;
            EquipmentID = equipmentID;
            Name = name;
            Condition = condition;
            PurchaseDate = purchaseDate;
            LastMaintenanceDate = lastMaintenanceDate;
            EquipmentImage = equipemtImage;
            AddedByUserID = addedByUserID;
            UserInfo = clsUser.FindByUserID(addedByUserID);
        }

        public static clsEquipment Find(int EquipmentID)
        {
            string Name = "";  byte Condition = (byte)enCondition.Unbroken;
            DateTime PurchaseDate = DateTime.Now;
            DateTime LastMaintenanceDate = DateTime.MaxValue;
            int AddedByUserID = -1; string EquipmentImage = "";

            bool IsFound = clsEquipmentsData.GetEquipmentByID(EquipmentID
                , ref Name, ref PurchaseDate, ref Condition, ref LastMaintenanceDate
                , ref EquipmentImage, ref AddedByUserID);

            if(IsFound) return new clsEquipment(EquipmentID, Name
                , (enCondition)Condition, PurchaseDate, LastMaintenanceDate
                , EquipmentImage, AddedByUserID);
            else return null;
        }

        public static clsEquipment Find(string Name)
        {
            int EquipmentID = -1; byte Condition = (byte)enCondition.Unbroken;
            DateTime PurchaseDate = DateTime.Now;
            DateTime LastMaintenanceDate = DateTime.MaxValue;
            int AddedByUserID = -1; string EquipmentImage = "";

            bool IsFound = clsEquipmentsData.GetEquipmentByName(Name
                , ref EquipmentID, ref PurchaseDate, ref Condition, ref LastMaintenanceDate
                , ref EquipmentImage, ref AddedByUserID);

            if (IsFound) return new clsEquipment(EquipmentID, Name
                , (enCondition)Condition, PurchaseDate, LastMaintenanceDate
                , EquipmentImage, AddedByUserID);
            else return null;
        }

        private bool _AddNewEquipment() 
        {
            this.EquipmentID = clsEquipmentsData.AddNewEquipment(Name, PurchaseDate
                , (byte)Condition, LastMaintenanceDate, EquipmentImage, AddedByUserID);
            return EquipmentID != -1;
        }

        private bool _UpdateEquipment()
        {
            return clsEquipmentsData.UpdateEquipment(EquipmentID, Name, PurchaseDate
                , (byte)Condition, LastMaintenanceDate, EquipmentImage, AddedByUserID);
        }

        public bool Save() 
        {
            switch (Mode) 
            {
                case enMode.AddNew: 
                    {
                        if (_AddNewEquipment()) 
                        {
                            Mode = enMode.Update;
                            return true;
                        }
                        else 
                        {
                            return false;
                        }
                    }
                case enMode.Update: 
                    {
                        return _UpdateEquipment();
                    }
            }return false;
        }

        public static bool DeleteEquipment(int EquipmentID) 
        {
            return clsEquipmentsData.DeleteEquipment(EquipmentID);
        }

        public static DataTable GetAllEquipments() 
        {
            return clsEquipmentsData.GetAllEquipments();
        }

        public bool UpdateLastMaintenanceDate(DateTime LastMaintenanceDate, byte Condition, int AddedByUserID) 
        {
            return clsEquipmentsData.UpdateLasMaintenanceDate(this.EquipmentID
                , LastMaintenanceDate, Condition, AddedByUserID);
        }
        public static bool IsEquipmentExist(int EquipmentID) 
        {
            return clsEquipmentsData.IsEquipmentExist(EquipmentID);
        }
    }
}
