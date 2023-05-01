using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace LoxleyOrbit.FaceScan.Models
{
    public enum HospitalFileStatus
    {
        Unknown,
        Cancel,
        Expired,
        Active
    }

    public enum RightGroup
    {
        None = 0,
        [Description("ประกันสุขภาพถ้วนหน้า")]
        SI2_302 = 1,                 // สิทธิปกส2 / 302/ แรงงานต่างด้าว / บุคคลผู้มีปัญหาสถานะสิทธิ
        [Description("ผู้ป่วยทั่วไป")]
        GeneralPatient = 2,        // ผู้ป่วยทั่วไป
        [Description("ข้าราชการและครอบครัว")]
        OFC = 3,                  // เบิกจ่ายตรง
        [Description("ประกันสังคม")]
        SI1_301 = 4,               // ปกส 1 / 301
        [Description("ประกันสุขภาพถ้วนหน้า 301")]
        SI1_301S = 40,               // ประกันสุขภาพถ้วนหน้า 1 / 301
        [Description("เจ้าหน้าที่และครอบครัว")]
        EMP = 5,                   // เจ้าหน้าที่ / ครอบครัวเจ้าหน้าที่ / นิสิตแพทย์ / นักศึกษาพยาบาล
        [Description("อปท")]
        DLA = 6,                   // อปท
        SI2_302_Expired = 7,       // ผู้ป่วยเคยมีสิทธิ ปกส2 / 302 / แรงงานต่างด้าว / บุคคลผู้มีปัญหาสถานะและสิทธิ
        SI1_301_Expired = 8,       // ผู้ป่วยเคยมีสิทธิ ปกส1 / 301
        SI2_302_ContactStaff = 9,  // ผู้ป่วยสิทธิ ปกส2 / 302 ที่ต้องติดต่อเจ้าหน้าที่ทุกครั้งก่อนอนุมัติสิทธิ เช่น ปกส2 ทุพลภาพ
        DLA_Expired = 10,          // เคยใช้สิทธิอปท หมดอายุแล้ว
        [Description("สิทธิอื่นๆ")]
        OTHER = 11,
        [Description("ไม่ชำระผ่าน Kiosk Payments")]
        Ignore = 99,
    }

    public enum UserType
    {
        Remote = 1,
        Report = 2
    }
}
