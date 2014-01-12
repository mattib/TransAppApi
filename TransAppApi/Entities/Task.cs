using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransAppApi.Entities
{
    public class Task
    {
        public Task()
        { }

        public Task(Task task)
        {
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
            ReciverComment = task.ReciverComment;
            //Contact = new Contact(task.Contact);
            RowStatus = task.RowStatus;
            TaskType = task.TaskType;
            DataExtention = task.DataExtention;
            SignatureId = task.SignatureId;
            if (task.ImageId != null)
            {
                ImageId = new string[task.ImageId.Length];
                Array.Copy(task.ImageId, ImageId, task.ImageId.Length);
            }
            else
            {
                ImageId = new string[0];
            }
            
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
        //public Contact Contact { get; set; }
        public int RowStatus { get; set; }
        public int? TaskType { get; set; }
        public string DataExtention { get; set; }
        public string SignatureId { get; set; }
        public string[] ImageId { get; set; }
        
        public bool? Rejected { get; set; }

    }
}