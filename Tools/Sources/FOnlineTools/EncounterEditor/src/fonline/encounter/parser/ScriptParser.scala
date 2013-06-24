package fonline.encounter.parser

import  fonline.parser._

/**
 *
 * @author mikewall
 * @since 15.10.2012, 9:46
 * @version 1.0
 */
class ScriptParser(val serverPath: String)
        extends GroupParser
        with TeamParser
        with DialogParser
        with ItemProtoParser
        with TableParser
        with TerrainParser
        with ZoneParser
        with ConfigParser
        with VarParser
        with AIParser
        with RoleParser
        with ParamParser
        with BagParser