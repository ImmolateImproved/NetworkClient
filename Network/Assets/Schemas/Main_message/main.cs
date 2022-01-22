// <auto-generated>
//  automatically generated by the FlatBuffers compiler, do not modify
// </auto-generated>

namespace Main_message
{

using global::System;
using global::System.Collections.Generic;
using global::FlatBuffers;

public struct main : IFlatbufferObject
{
  private Table __p;
  public ByteBuffer ByteBuffer { get { return __p.bb; } }
  public static void ValidateVersion() { FlatBufferConstants.FLATBUFFERS_2_0_0(); }
  public static main GetRootAsmain(ByteBuffer _bb) { return GetRootAsmain(_bb, new main()); }
  public static main GetRootAsmain(ByteBuffer _bb, main obj) { return (obj.__assign(_bb.GetInt(_bb.Position) + _bb.Position, _bb)); }
  public void __init(int _i, ByteBuffer _bb) { __p = new Table(_i, _bb); }
  public main __assign(int _i, ByteBuffer _bb) { __init(_i, _bb); return this; }

  public uint MessageId { get { int o = __p.__offset(4); return o != 0 ? __p.bb.GetUint(o + __p.bb_pos) : (uint)0; } }
  public bool IsReq { get { int o = __p.__offset(6); return o != 0 ? 0!=__p.bb.Get(o + __p.bb_pos) : (bool)false; } }
  public Main_message.message_type MesType { get { int o = __p.__offset(8); return o != 0 ? (Main_message.message_type)__p.bb.Get(o + __p.bb_pos) : Main_message.message_type.Register; } }
  public byte Data(int j) { int o = __p.__offset(10); return o != 0 ? __p.bb.Get(__p.__vector(o) + j * 1) : (byte)0; }
  public int DataLength { get { int o = __p.__offset(10); return o != 0 ? __p.__vector_len(o) : 0; } }
#if ENABLE_SPAN_T
  public Span<byte> GetDataBytes() { return __p.__vector_as_span<byte>(10, 1); }
#else
  public ArraySegment<byte>? GetDataBytes() { return __p.__vector_as_arraysegment(10); }
#endif
  public byte[] GetDataArray() { return __p.__vector_as_array<byte>(10); }

  public static Offset<Main_message.main> Createmain(FlatBufferBuilder builder,
      uint message_id = 0,
      bool is_req = false,
      Main_message.message_type mes_type = Main_message.message_type.Register,
      VectorOffset dataOffset = default(VectorOffset)) {
    builder.StartTable(4);
    main.AddData(builder, dataOffset);
    main.AddMessageId(builder, message_id);
    main.AddMesType(builder, mes_type);
    main.AddIsReq(builder, is_req);
    return main.Endmain(builder);
  }

  public static void Startmain(FlatBufferBuilder builder) { builder.StartTable(4); }
  public static void AddMessageId(FlatBufferBuilder builder, uint messageId) { builder.AddUint(0, messageId, 0); }
  public static void AddIsReq(FlatBufferBuilder builder, bool isReq) { builder.AddBool(1, isReq, false); }
  public static void AddMesType(FlatBufferBuilder builder, Main_message.message_type mesType) { builder.AddByte(2, (byte)mesType, 0); }
  public static void AddData(FlatBufferBuilder builder, VectorOffset dataOffset) { builder.AddOffset(3, dataOffset.Value, 0); }
  public static VectorOffset CreateDataVector(FlatBufferBuilder builder, byte[] data) { builder.StartVector(1, data.Length, 1); for (int i = data.Length - 1; i >= 0; i--) builder.AddByte(data[i]); return builder.EndVector(); }
  public static VectorOffset CreateDataVectorBlock(FlatBufferBuilder builder, byte[] data) { builder.StartVector(1, data.Length, 1); builder.Add(data); return builder.EndVector(); }
  public static void StartDataVector(FlatBufferBuilder builder, int numElems) { builder.StartVector(1, numElems, 1); }
  public static Offset<Main_message.main> Endmain(FlatBufferBuilder builder) {
    int o = builder.EndTable();
    return new Offset<Main_message.main>(o);
  }
  public static void FinishmainBuffer(FlatBufferBuilder builder, Offset<Main_message.main> offset) { builder.Finish(offset.Value); }
  public static void FinishSizePrefixedmainBuffer(FlatBufferBuilder builder, Offset<Main_message.main> offset) { builder.FinishSizePrefixed(offset.Value); }
};


}
