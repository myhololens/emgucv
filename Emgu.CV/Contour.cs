using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Emgu.CV
{
   /// <summary>
   /// Wrapped class for Contour
   /// </summary>
   public class Contour<T> : Seq<T> where T : struct
   {
      /// <summary>
      /// Craete a contour from the specific IntPtr and storage
      /// </summary>
      /// <param name="ptr"></param>
      /// <param name="storage"></param>
      public Contour(IntPtr ptr, MemStorage storage)
         : base(ptr, storage)
      {
      }

      /// <summary>
      /// Create a contour using the specific <paramref name="seqFlag"/> and <paramref name="storage"/>
      /// </summary>
      /// <param name="seqFlag">Flags of the created contour. If the contour is not passed to any function working with a specific type of sequences, the sequence value may be set to 0, otherwise the appropriate type must be selected from the list of predefined contour types</param>
      /// <param name="storage">the storage</param>
      public Contour(int seqFlag, MemStorage storage)
         : this(IntPtr.Zero, storage)
      {
         _ptr = CvInvoke.cvCreateSeq(
             seqFlag, Marshal.SizeOf(typeof(MCvContour)),
             Marshal.SizeOf(typeof(T)),
             storage.Ptr);
      }

      /// <summary>
      /// Create a contour of the specific kind, type and flag
      /// </summary>
      /// <param name="kind">The kind of the sequence</param>
      /// <param name="eltype">The type of the sequence</param>
      /// <param name="flag">The flag of the sequence</param>
      /// <param name="stor">The storage</param>
      public Contour(CvEnum.SEQ_ELTYPE eltype, CvEnum.SEQ_KIND kind,  CvEnum.SEQ_FLAG flag, MemStorage stor)
         : this( ((int)kind | (int)eltype | (int)flag), stor)
      {
      }

      /// <summary>
      /// Create a contour using the specific <paramref name="storage"/>
      /// </summary>
      /// <param name="storage">The storage to be used</param>
      public Contour(MemStorage storage)
         : this((int)CvEnum.SEQ_TYPE.CV_SEQ_POLYGON , storage)
      {
      }

      /// <summary>
      /// Return the MCvContour structure
      /// </summary>
      public MCvContour MCvContour
      {
         get
         {
            return (MCvContour)Marshal.PtrToStructure(Ptr, typeof(MCvContour));
         }
      }

      /// <summary>
      /// Same as h_next pointer in CvSeq
      /// </summary>
      public new Contour<T> HNext
      {
         get
         {
            MCvContour seq = MCvContour;
            return seq.h_next == IntPtr.Zero ? null : new Contour<T>(seq.h_next, Storage);
         }
         set
         {
            MCvContour seq = MCvContour;
            seq.h_next = value == null ? IntPtr.Zero : value.Ptr;
            Marshal.StructureToPtr(seq, _ptr, false);
         }
      }

      /// <summary>
      /// Same as h_prev pointer in CvSeq
      /// </summary>
      public new Contour<T> HPrev
      {
         get
         {
            MCvContour seq = MCvContour;
            return seq.h_prev == IntPtr.Zero ? null : new Contour<T>(seq.h_prev, Storage);
         }
         set
         {
            MCvContour seq = MCvContour;
            seq.h_prev = value == null ? IntPtr.Zero : value.Ptr;
            Marshal.StructureToPtr(seq, _ptr, false);
         }
      }

      /// <summary>
      /// Same as v_next pointer in CvSeq
      /// </summary>
      public new Contour<T> VNext
      {
         get
         {
            MCvContour seq = MCvContour;
            return seq.v_next == IntPtr.Zero ? null : new Contour<T>(seq.v_next, Storage);
         }
         set
         {
            MCvContour seq = MCvContour;
            seq.v_next = value == null ? IntPtr.Zero : value.Ptr;
            Marshal.StructureToPtr(seq, _ptr, false);
         }
      }

      /// <summary>
      /// Same as v_prev pointer in CvSeq
      /// </summary>
      public new Contour<T> VPrev
      {
         get
         {
            MCvContour seq = MCvContour;
            return seq.v_prev == IntPtr.Zero ? null : new Contour<T>(seq.v_prev, Storage);
         }
         set
         {
            MCvContour seq = MCvContour;
            seq.v_prev = value == null ? IntPtr.Zero : value.Ptr;
            Marshal.StructureToPtr(seq, _ptr, false);
         }
      }

      /// <summary>
      /// Approximates one curves and returns the approximation result. 
      /// </summary>
      /// <param name="accuracy">The desired approximation accuracy</param>
      /// <param name="storage"> The storage the resulting sequence use</param>
      /// <returns>The approximated contour</returns>
      public new Contour<T> ApproxPoly(double accuracy, MemStorage storage)
      {
         return ApproxPoly(accuracy, 0, storage);
      }

      /// <summary>
      /// Approximates one or more curves and returns the approximation result[s]. In case of multiple curves approximation the resultant tree will have the same structure as the input one (1:1 correspondence)
      /// </summary>
      /// <param name="accuracy">The desired approximation accuracy</param>
      /// <param name="storage"> The storage the resulting sequence use</param>
      /// <param name="maxLevel">
      /// Maximal level for contour approximation. 
      /// If 0, only contour is arrpoximated. 
      /// If 1, the contour and all contours after it on the same level are approximated. 
      /// If 2, all contours after and all contours one level below the contours are approximated, etc. If the value is negative, the function does not draw the contours following after contour but draws child contours of contour up to abs(maxLevel)-1 level
      /// </param>
      /// <returns>The approximated contour</returns>
      public new Contour<T> ApproxPoly(double accuracy, int maxLevel, MemStorage storage)
      {
         return new Contour<T>(
             CvInvoke.cvApproxPoly(
             Ptr,
             System.Runtime.InteropServices.Marshal.SizeOf(typeof(MCvContour)),
             storage.Ptr,
             CvEnum.APPROX_POLY_TYPE.CV_POLY_APPROX_DP,
             accuracy,
             maxLevel),
             storage);
      }

      /// <summary>
      /// Approximates one or more curves and returns the approximation result[s]. In case of multiple curves approximation the resultant tree will have the same structure as the input one (1:1 correspondence)
      /// </summary>
      /// <param name="accuracy">The desired approximation accuracy</param>
      /// <returns>The approximated contour</returns>
      public new Contour<T> ApproxPoly(double accuracy)
      {
         MemStorage storage = new MemStorage();
         return ApproxPoly(accuracy, storage);
      }
   }
}
