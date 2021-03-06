// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace messages_shot
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Shot : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Shot GetRootAsShot(ByteBuffer _bb) { return GetRootAsShot(_bb, new Shot()); }
  public static Shot GetRootAsShot(ByteBuffer _bb, Shot obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Shot __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public messages_shot.weapon_type WType { get { int o = __p.__offset(4); return o != 0 ? (messages_shot.weapon_type)__p.bb.GetSbyte(o + __p.bb_pos) : messages_shot.weapon_type.laser; } }
  public float X { get { int o = __p.__offset(6); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }
  public float Y { get { int o = __p.__offset(8); return o != 0 ? __p.bb.GetFloat(o + __p.bb_pos) : (float)0.0f; } }

  public static Offset<messages_shot.Shot> CreateShot(FlatBufferBuilder builder,
      messages_shot.weapon_type w_type = messages_shot.weapon_type.laser,
      float x = 0.0f,
      float y = 0.0f) {
    builder.StartTable(3);
    Shot.AddY(builder, y);
    Shot.AddX(builder, x);
    Shot.AddWType(builder, w_type);
    return Shot.EndShot(builder);
  }

  public static void StartShot(FlatBufferBuilder builder) { builder.StartTable(3); }
  public static void AddWType(FlatBufferBuilder builder, messages_shot.weapon_type wType) { builder.AddSbyte(0, (sbyte)wType, 0); }
  public static void AddX(FlatBufferBuilder builder, float x) { builder.AddFloat(1, x, 0.0f); }
  public static void AddY(FlatBufferBuilder builder, float y) { builder.AddFloat(2, y, 0.0f); }
  public static Offset<messages_shot.Shot> EndShot(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<messages_shot.Shot>(o);
  }
  public static void FinishShotBuffer(FlatBufferBuilder builder, Offset<messages_shot.Shot> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedShotBuffer(FlatBufferBuilder builder, Offset<messages_shot.Shot> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
