// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace messages_shots
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct Shots : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static Shots GetRootAsShots(ByteBuffer _bb) { return GetRootAsShots(_bb, new Shots()); }
  public static Shots GetRootAsShots(ByteBuffer _bb, Shots obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public Shots __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public messages_shots.Shot? Objects(int j) { int o = __p.__offset(4); return o != 0 ? (messages_shots.Shot?)(new messages_shots.Shot()).__assign(__p.__indirect(__p.__vector(o) + j * 4), __p.bb) : null; }
  public int ObjectsLength { get { int o = __p.__offset(4); return o != 0 ? __p.__vector_len(o) : 0; } }

  public static Offset<messages_shots.Shots> CreateShots(FlatBufferBuilder builder,
      VectorOffset objectsOffset = default(VectorOffset)) {
    builder.StartTable(1);
    Shots.AddObjects(builder, objectsOffset);
    return Shots.EndShots(builder);
  }

  public static void StartShots(FlatBufferBuilder builder) { builder.StartTable(1); }
  public static void AddObjects(FlatBufferBuilder builder, VectorOffset objectsOffset) { builder.AddOffset(0, objectsOffset.Value, 0); }
  public static VectorOffset CreateObjectsVector(FlatBufferBuilder builder, Offset<messages_shots.Shot>[] data) { builder.StartVector(4, data.Length, 4); for (int i = data.Length - 1; i >= 0; i--) builder.AddOffset(data[i].Value); return builder.EndVector(); }
  public static VectorOffset CreateObjectsVectorBlock(FlatBufferBuilder builder, Offset<messages_shots.Shot>[] data) { builder.StartVector(4, data.Length, 4); builder.Add(data); return builder.EndVector(); }
  public static void StartObjectsVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(4, numElems, 4); }
  public static Offset<messages_shots.Shots> EndShots(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<messages_shots.Shots>(o);
  }
  public static void FinishShotsBuffer(FlatBufferBuilder builder, Offset<messages_shots.Shots> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedShotsBuffer(FlatBufferBuilder builder, Offset<messages_shots.Shots> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
