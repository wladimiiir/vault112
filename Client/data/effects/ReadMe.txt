///////////////////////////////////////////////////////////////////////////////////////////////
// FOnline effects specifications
///////////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////////
// Default effects
///////////////////////////////////////////////////////////////////////////////////////////////

 Type              File name
2D sprites        2D_Default.fx
3D models         3D_Default.fx
Interface         Interface_Default.fx
Fonts             Font_Default.fx
Primitive         Primitive_Default.fx

///////////////////////////////////////////////////////////////////////////////////////////////
// Structures pipeline
// Structures declared in IOStructures.inc
///////////////////////////////////////////////////////////////////////////////////////////////

 Effect              Application to VS           VS to PS                   PS output
2D Game sprites:    AppToVsToPs_2DEgg        -> AppToVsToPs_2DEgg       -> float4 : COLOR
Interface sprites:  AppToVsToPs_2D           -> AppToVsToPs_2D          -> float4 : COLOR
Font sprites:       AppToVsToPs_2D           -> AppToVsToPs_2D          -> float4 : COLOR
Primitives:         AppToVsToPs_2DPrimitive  -> AppToVsToPs_2DPrimitive -> float4 : COLOR
3D Simple:          AppToVs_3D               -> VsToPs_3D               -> float4 : COLOR
3D Simple+Tangent:  AppToVs_3DTangent        -> VsToPs_3DTangent        -> float4 : COLOR
3D Skinned:         AppToVs_3DSkinned        -> VsToPs_3D               -> float4 : COLOR
3D Skinned+Tangent: AppToVs_3DSkinnedTangent -> VsToPs_3DTangent        -> float4 : COLOR

///////////////////////////////////////////////////////////////////////////////////////////////
// Effect variables
// Engine updates it automatically
// Include/exclude as you wish, this detecting automatically
///////////////////////////////////////////////////////////////////////////////////////////////

// 2D/Interface/Font variables

sampler2D ColorMap;                   // Main texture
sampler2D EggColor;                   // Egg texture, if this sampler not used than
                                      // TexEggCoord will be equal to zero (x or y)

// 3D variables

texture   Texture[0..9]               // Attached textures, zero is default
float4x3  WorldMatrices[MaxMatrices]; // World matrices, first used for simple meshes
float4x4  ViewProjMatrix;             // View * Projection matrix
sampler2D MainTexture;                // Main texture
int       BonesInfluences;            // Bones per vertex, for skinned meshes
float4    GroundPosition;             // Ground position
float4    LightDir;                   // Light direction 
float4    LightDiffuse;               // Light diffuse color
float4    MaterialAmbient;            // Ambient color
float4    MaterialDiffuse;            // Diffuse color

// Generic variables

float PassIndex;                      // Number of current pass
float Time;                           // Current time [0,120), in seconds, cycled
float TimeGame;                       // Current game time [0,120), in seconds, cycled
float Random1Pass;                    // Random value [0,1], value updated each pass
float Random2Pass;                    // Random value [0,1], value updated each pass
float Random3Pass;                    // Random value [0,1], value updated each pass
float Random4Pass;                    // Random value [0,1], value updated each pass
float Random1Effect;                  // Random value [0,1], value updated each effect
float Random2Effect;                  // Random value [0,1], value updated each effect
float Random3Effect;                  // Random value [0,1], value updated each effect
float Random4Effect;                  // Random value [0,1], value updated each effect
float EffectValue[0..9];              // Value that changed in scripts, names is same
float AnimPosProc;                    // Current animation process 0%..100% [0,1)
float AnimPosTime;                    // Current animation time, in seconds

///////////////////////////////////////////////////////////////////////////////////////////////
// 
///////////////////////////////////////////////////////////////////////////////////////////////

