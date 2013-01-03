#ifndef __FONLINE_TLA__
#define __FONLINE_TLA__

// Script constants
#define SKIP_PRAGMAS
#include "_defines.fos"

// Disable macro redefinition warning
#pragma warning (push)
#pragma warning (disable : 4005)
#include "fonline.h"
#pragma warning (pop)

// Script global variables
struct _GlobalVars
{
    int*  CurX;
    int*  CurY;
    uint* HitAimEyes;
    uint* HitAimHead;
    uint* HitAimGroin;
    uint* HitAimTorso;
    uint* HitAimArms;
    uint* HitAimLegs;
} extern GlobalVars;

#endif // __FONLINE_TLA__
