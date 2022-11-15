#include "pch-c.h"
#ifndef _MSC_VER
# include <alloca.h>
#else
# include <malloc.h>
#endif


#include "codegen/il2cpp-codegen-metadata.h"





// 0x00000001 System.String UnityEngine.TextMesh::get_text()
extern void TextMesh_get_text_mB3E900AED17390DE50DFC984428BC29EB1CA60A2 (void);
// 0x00000002 System.Void UnityEngine.TextMesh::set_text(System.String)
extern void TextMesh_set_text_mDF79D39638ED82797D0B0B3BB9E6B10712F8EA9E (void);
// 0x00000003 System.Void UnityEngine.TextMesh::set_anchor(UnityEngine.TextAnchor)
extern void TextMesh_set_anchor_m3FCB7C4B1FF66CE189B56076C0306AFE984FCD32 (void);
// 0x00000004 System.Single UnityEngine.TextMesh::get_characterSize()
extern void TextMesh_get_characterSize_mA9495F227772CFEBB2EB64B65166E682DB5E8147 (void);
// 0x00000005 System.Void UnityEngine.TextMesh::set_characterSize(System.Single)
extern void TextMesh_set_characterSize_mAEAE87C4648EF49409BDA93E5F504356B68D6052 (void);
// 0x00000006 UnityEngine.Color UnityEngine.TextMesh::get_color()
extern void TextMesh_get_color_m128E5D16AA72D5284C70957253DEAEE4FBEB023E (void);
// 0x00000007 System.Void UnityEngine.TextMesh::set_color(UnityEngine.Color)
extern void TextMesh_set_color_mF08F30C3CD797C16289225B567724B9F07DC641E (void);
// 0x00000008 System.Void UnityEngine.TextMesh::get_color_Injected(UnityEngine.Color&)
extern void TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769 (void);
// 0x00000009 System.Void UnityEngine.TextMesh::set_color_Injected(UnityEngine.Color&)
extern void TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4 (void);
// 0x0000000A System.Void UnityEngine.Font::add_textureRebuilt(System.Action`1<UnityEngine.Font>)
extern void Font_add_textureRebuilt_m0C7E9998192691918BC92548EE955380AD63FF0B (void);
// 0x0000000B System.Void UnityEngine.Font::remove_textureRebuilt(System.Action`1<UnityEngine.Font>)
extern void Font_remove_textureRebuilt_mCCA3614ED92E2BE1EAC5FCE2D7DDFEDB0DCDE127 (void);
// 0x0000000C UnityEngine.Material UnityEngine.Font::get_material()
extern void Font_get_material_m61ABDEC14C6D659DDC5A4F080023699116C17364 (void);
// 0x0000000D System.Void UnityEngine.Font::.ctor()
extern void Font__ctor_m9106C7F312AE77F6721001A5A3143951201AC841 (void);
// 0x0000000E System.Void UnityEngine.Font::InvokeTextureRebuilt_Internal(UnityEngine.Font)
extern void Font_InvokeTextureRebuilt_Internal_m86017C5A7B49F602937D8C32FC978B876AFC37F9 (void);
// 0x0000000F System.Boolean UnityEngine.Font::HasCharacter(System.Char)
extern void Font_HasCharacter_m71A84FE036055880E1543D79A38FEFA495AD200B (void);
// 0x00000010 System.Boolean UnityEngine.Font::HasCharacter(System.Int32)
extern void Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18 (void);
// 0x00000011 System.Void UnityEngine.Font::Internal_CreateFont(UnityEngine.Font,System.String)
extern void Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F (void);
// 0x00000012 System.Void UnityEngine.Font/FontTextureRebuildCallback::.ctor(System.Object,System.IntPtr)
extern void FontTextureRebuildCallback__ctor_m1AF27FC83F3136E493F47015F99CE7A4E6BCA0BC (void);
// 0x00000013 System.Void UnityEngine.Font/FontTextureRebuildCallback::Invoke()
extern void FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D (void);
static Il2CppMethodPointer s_methodPointers[19] = 
{
	TextMesh_get_text_mB3E900AED17390DE50DFC984428BC29EB1CA60A2,
	TextMesh_set_text_mDF79D39638ED82797D0B0B3BB9E6B10712F8EA9E,
	TextMesh_set_anchor_m3FCB7C4B1FF66CE189B56076C0306AFE984FCD32,
	TextMesh_get_characterSize_mA9495F227772CFEBB2EB64B65166E682DB5E8147,
	TextMesh_set_characterSize_mAEAE87C4648EF49409BDA93E5F504356B68D6052,
	TextMesh_get_color_m128E5D16AA72D5284C70957253DEAEE4FBEB023E,
	TextMesh_set_color_mF08F30C3CD797C16289225B567724B9F07DC641E,
	TextMesh_get_color_Injected_m3E6F8CA2677A2304BCF58A8FCE4468B29D97F769,
	TextMesh_set_color_Injected_mC7C51FAE003F1B2FB510F50DC1C4AFBFE740B4E4,
	Font_add_textureRebuilt_m0C7E9998192691918BC92548EE955380AD63FF0B,
	Font_remove_textureRebuilt_mCCA3614ED92E2BE1EAC5FCE2D7DDFEDB0DCDE127,
	Font_get_material_m61ABDEC14C6D659DDC5A4F080023699116C17364,
	Font__ctor_m9106C7F312AE77F6721001A5A3143951201AC841,
	Font_InvokeTextureRebuilt_Internal_m86017C5A7B49F602937D8C32FC978B876AFC37F9,
	Font_HasCharacter_m71A84FE036055880E1543D79A38FEFA495AD200B,
	Font_HasCharacter_mAB838A26F002CB5E4B4DB297F7D6836A28625B18,
	Font_Internal_CreateFont_mF7CE69E4C8DFB953EA74F262DE3D28B83C4DF22F,
	FontTextureRebuildCallback__ctor_m1AF27FC83F3136E493F47015F99CE7A4E6BCA0BC,
	FontTextureRebuildCallback_Invoke_m8B52C3F4823ADBB80062209E6BA2B33202AE958D,
};
static const int32_t s_InvokerIndices[19] = 
{
	2928,
	2459,
	2444,
	2960,
	2486,
	2877,
	2413,
	2404,
	2404,
	4585,
	4585,
	2928,
	3002,
	4585,
	1825,
	1745,
	4215,
	1380,
	3002,
};
IL2CPP_EXTERN_C const Il2CppCodeGenModule g_UnityEngine_TextRenderingModule_CodeGenModule;
const Il2CppCodeGenModule g_UnityEngine_TextRenderingModule_CodeGenModule = 
{
	"UnityEngine.TextRenderingModule.dll",
	19,
	s_methodPointers,
	0,
	NULL,
	s_InvokerIndices,
	0,
	NULL,
	0,
	NULL,
	0,
	NULL,
	NULL,
	NULL, // module initializer,
	NULL,
	NULL,
	NULL,
};
