using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TransAppApi.Entities;

namespace TransAppApi.Models
{
    public class MongoDbTask
    {
        private ObjectId m_mongoId;

        public MongoDbTask()
        {
        }

        public MongoDbTask(Task task)
        {
            MongoId = new ObjectId(DateTime.UtcNow, 0, short.Parse(task.Id.ToString()), 0);
            Id = task.Id;
            DeliveryNumber = task.DeliveryNumber;
            UserId = task.UserId;
            CompanyId = task.CompanyId;
            SenderAddress = task.SenderAddress;
            ReciverAddress = task.ReciverAddress;
            TaskStatus = task.TaskStatus;
            Created = task.Created;
            PickedUpAt = task.PickedUpAt;
            DeliveredAt = task.DeliveredAt;
            PickUpTime = task.PickUpTime;
            DeliveryTime = task.DeliveryTime;
            LastModified = task.LastModified;
            SenderComment = task.SenderComment;
            ReciverComment = task.SenderAddress;
            //if (task.Contact != null)
            //{
            //    ContactId = task.Contact.Id;
            //}
            RowStatus = task.RowStatus;
            TaskType = task.TaskType;
            DataExtention = task.DataExtention;
            SignatureId = task.SignatureId;
            var imageString = string.Empty;
            if (task.ImageId != null)
            {
                foreach (var image in task.ImageId)
                {
                    imageString = imageString + image + ",";
                }
            }

            ImageId = imageString;
            Rejected = task.Rejected;
        }

        public int Id { get; set; }
        public string DeliveryNumber { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public string SenderAddress { get; set; }
        public string ReciverAddress { get; set; }
        public int TaskStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime? PickedUpAt { get; set; }
        public DateTime? DeliveredAt { get; set; }
        public DateTime? PickUpTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime LastModified { get; set; }
        public string SenderComment { get; set; }
        public string ReciverComment { get; set; }
        //public int ContactId { get; set; }
        public int RowStatus { get; set; }
        public int? TaskType { get; set; }
        public string DataExtention { get; set; }
        public string SignatureId { get; set; }
        public string ImageId { get; set; }
        
        public bool? Rejected { get; set; }
        public ObjectId MongoId
        {
            get { return m_mongoId; }
            set
            {
                m_mongoId = value;
                Id = (int)value.Pid;
            }
        }
    }
}