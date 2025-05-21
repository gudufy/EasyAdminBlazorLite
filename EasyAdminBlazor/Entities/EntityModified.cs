﻿using BootstrapBlazor.Components;
using FreeSql.DataAnnotations;
using MiniExcelLibs.Attributes;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EasyAdminBlazor;

/// <summary>
/// 修改接口
/// </summary>
public interface IEntityModified
{
    /// <summary>
    /// 修改者Id
    /// </summary>
    long? ModifiedUserId { get; set; }
    /// <summary>
    /// 修改者
    /// </summary>
    string ModifiedUserName { get; set; }
    /// <summary>
    /// 修改时间
    /// </summary>
    DateTime? ModifiedTime { get; set; }
}

/// <summary>
/// 实体修改
/// </summary>
public abstract class EntityModified<TKey> : EntityCreated<TKey>, IEntityModified
{
    /// <summary>
    /// 修改者Id
    /// </summary>
    [Column(Position = -12, CanInsert = false)]
    [JsonProperty(Order = 10000)]
    [JsonPropertyOrder(10000)]
    [ExcelColumn(Ignore = true)]
    [AutoGenerateColumn(Ignore = true)]
    public virtual long? ModifiedUserId { get; set; }

    /// <summary>
    /// 修改者
    /// </summary>
    [Column(Position = -11, CanInsert = false), MaxLength(50)]
    [JsonProperty(Order = 10001)]
    [JsonPropertyOrder(10001)]
    [AutoGenerateColumn(Order =-41, IsVisibleWhenAdd = false, IsVisibleWhenEdit = false,Visible =false)]
    [DisplayName("修改人")]
    public virtual string ModifiedUserName { get; set; }

    /// <summary>
    /// 修改时间
    /// </summary>
    [JsonProperty(Order = 10002)]
    [JsonPropertyOrder(10002)]
    [AutoGenerateColumn(Order = -51,Width =160, IsVisibleWhenAdd = false, IsVisibleWhenEdit = false, Visible = false)]
    [Column(Position = -10, CanInsert = false, ServerTime = DateTimeKind.Local)]
    [DisplayName("修改时间")]
    public virtual DateTime? ModifiedTime { get; set; }
}

/// <summary>
/// 实体修改
/// </summary>
public abstract class EntityModified : EntityModified<long>
{
}
