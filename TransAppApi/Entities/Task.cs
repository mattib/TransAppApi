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
            SenderAddressId = task.SenderAddressId;
            ReciverAddressId = task.ReciverAddressId;
            TaskStatus = task.TaskStatus;
            Created = task.Created;
            PickedUpAt = task.PickedUpAt;
            DeliveredAt = task.DeliveredAt;
            PickUpTime = task.PickUpTime;
            DeliveryTime = task.DeliveryTime;
            LastModified = task.LastModified;
            Accepted = task.Accepted;
            PackageType = task.PackageType;
            Comment = task.Comment;
            RejectionReason = task.RejectionReason;
            ContactId = task.ContactId;
            RowStatus = task.RowStatus;
            TaskType = task.TaskType;
            DataExtention = task.DataExtention;
        }

        public int Id { get; set; }
        public string DeliveryNumber { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int SenderAddressId { get; set; }
        public int ReciverAddressId { get; set; }
        public int TaskStatus { get; set; }
        public DateTime Created { get; set; }
        public DateTime PickedUpAt { get; set; }
        public DateTime DeliveredAt { get; set; }
        public DateTime PickUpTime { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime LastModified { get; set; }
        public bool Accepted { get; set; }
        public bool PackageType { get; set; }
        public string Comment { get; set; }
        public bool RejectionReason { get; set; }
        public int ContactId { get; set; }
        public int RowStatus { get; set; }
        public int TaskType { get; set; }
        public string DataExtention { get; set; }
    }
}