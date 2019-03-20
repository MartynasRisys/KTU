// Example Bot #1: The Reference Bot


/** This bot builds a 'direction value map' that assigns an attractiveness score to
  * each of the eight available 45-degree directions. Additional behaviors:
  * - aggressive missiles: approach an enemy master, then explode
  * - defensive missiles: approach an enemy slave and annihilate it
  *
  * The master bot uses the following state parameters:
  *  - dontFireAggressiveMissileUntil
  *  - dontFireDefensiveMissileUntil
  *  - lastDirection
  * The mini-bots use the following state parameters:
  *  - mood = Aggressive | Defensive | Lurking
  *  - target = remaining offset to target location
  */
import Array._
import scala.collection.mutable.ListBuffer
object ControlFunction
{
    def forMaster(bot: Bot) {
        // demo: log the view of the master bot into the debug output (if running in the browser sandbox)
        //bot.log(bot.view.cells.grouped(31).mkString("\n"))

        val (directionValue, nearestEnemyMaster, nearestEnemySlave) = analyzeViewAsMaster(bot)

        val dontFireAggressiveMissileUntil = bot.inputAsIntOrElse("dontFireAggressiveMissileUntil", -1)
        val dontFireDefensiveMissileUntil = bot.inputAsIntOrElse("dontFireDefensiveMissileUntil", -1)
        val lastDirection = bot.inputAsIntOrElse("lastDirection", 0)

        // determine movement direction
        //directionValue(lastDirection) += 10 // try to break ties by favoring the last direction
        //val bestDirection45 = directionValue.zipWithIndex.maxBy(_._1)._2
        //val direction = XY.fromDirection45(bestDirection45)
        bot.move(directionValue)
       // bot.set("lastDirection" -> bestDirection45)

        if(dontFireAggressiveMissileUntil < bot.time && bot.energy > 100) { // fire attack missile?
            nearestEnemyMaster match {
                case None =>            // no-on nearby
                case Some(relPos) =>    // a master is nearby
                    val unitDelta = relPos.signum
                    val remainder = relPos - unitDelta // we place slave nearer target, so subtract that from overall delta
                    bot.spawn(unitDelta, "mood" -> "Aggressive", "target" -> remainder)
                    bot.set("dontFireAggressiveMissileUntil" -> (bot.time + relPos.stepCount + 1))
            }
        }
        else
        if(dontFireDefensiveMissileUntil < bot.time && bot.energy > 100) { // fire defensive missile?
            nearestEnemySlave match {
                case None =>            // no-on nearby
                case Some(relPos) =>    // an enemy slave is nearby
                    if(relPos.stepCount < 8) {
                        // this one's getting too close!
                        val unitDelta = relPos.signum
                        val remainder = relPos - unitDelta // we place slave nearer target, so subtract that from overall delta
                        bot.spawn(unitDelta, "mood" -> "Defensive", "target" -> remainder)
                        bot.set("dontFireDefensiveMissileUntil" -> (bot.time + relPos.stepCount + 1))
                    }
            }
        }
    }


    def forSlave(bot: MiniBot) {
        bot.inputOrElse("mood", "Lurking") match {
            case "Aggressive" => reactAsAggressiveMissile(bot)
            case "Defensive" => reactAsDefensiveMissile(bot)
            case s: String => bot.log("unknown mood: " + s)
        }
    }


    def reactAsAggressiveMissile(bot: MiniBot) {
        bot.view.offsetToNearest('m') match {
            case Some(delta: XY) =>
                // another master is visible at the given relative position (i.e. position delta)

                // close enough to blow it up?
                if(delta.length <= 2) {
                    // yes -- blow it up!
                    bot.explode(4)

                } else {
                    // no -- move closer!
                    bot.move(delta.signum)
                    bot.set("rx" -> delta.x, "ry" -> delta.y)
                }
            case None =>
                // no target visible -- follow our targeting strategy
                val target = bot.inputAsXYOrElse("target", XY.Zero)

                // did we arrive at the target?
                if(target.isNonZero) {
                    // no -- keep going
                    val unitDelta = target.signum // e.g. CellPos(-8,6) => CellPos(-1,1)
                    bot.move(unitDelta)

                    // compute the remaining delta and encode it into a new 'target' property
                    val remainder = target - unitDelta // e.g. = CellPos(-7,5)
                    bot.set("target" -> remainder)
                } else {
                    // yes -- but we did not detonate yet, and are not pursuing anything?!? => switch purpose
                    bot.set("mood" -> "Lurking", "target" -> "")
                    bot.say("Lurking")
                }
        }
    }


    def reactAsDefensiveMissile(bot: MiniBot) {
        bot.view.offsetToNearest('s') match {
            case Some(delta: XY) =>
                // another slave is visible at the given relative position (i.e. position delta)
                // move closer!
                bot.move(delta.signum)
                bot.set("rx" -> delta.x, "ry" -> delta.y)

            case None =>
                // no target visible -- follow our targeting strategy
                val target = bot.inputAsXYOrElse("target", XY.Zero)

                // did we arrive at the target?
                if(target.isNonZero) {
                    // no -- keep going
                    val unitDelta = target.signum // e.g. CellPos(-8,6) => CellPos(-1,1)
                    bot.move(unitDelta)

                    // compute the remaining delta and encode it into a new 'target' property
                    val remainder = target - unitDelta // e.g. = CellPos(-7,5)
                    bot.set("target" -> remainder)
                } else {
                    // yes -- but we did not annihilate yet, and are not pursuing anything?!? => switch purpose
                    bot.set("mood" -> "Lurking", "target" -> "")
                    bot.say("Lurking")
                }
        }
    }


    /** Analyze the view, building a map of attractiveness for the 45-degree directions and
      * recording other relevant data, such as the nearest elements of various kinds.
      */
    def analyzeViewAsMaster(bot: Bot) = {
        val view: View = bot.view
        var directionValue : XY = XY.Down
        var nearestEnemyMaster: Option[XY] = None
        var nearestEnemySlave: Option[XY] = None
        var cellDetails = ofDim[XY](31,31)
        var closedList = ofDim[Boolean](31,31)
        val cells = view.cells
        val cellCount = cells.length
        var src: XY = new XY(15, 15)
        src.f = 0.0
        src.g = 0.0
        src.h = 0.0
        src.parentROW = 15
        src.parentCOL = 15
        src.f = 0
        cellDetails(15)(15) = src
        var dest: XY = new XY(-1, -1)
        for(i <- 0 until cellCount) {
            val cellRelPos = view.relPosFromIndex(i)
            
            if(cellRelPos.isNonZero) {
                
                var currentCOL = i % 31 
                var currentROW = i / 31
                var addidionalF = 0.0
                val distance = src.distanceTo(XY(currentCOL, currentROW))
                //bot.log(currentY.toString)
                closedList(currentCOL)(currentROW) = false
                val tempXY = new XY(currentCOL, currentROW)
                if (currentCOL == 15 && currentROW == 15) bot.log("as tevas")
                val additionalF = cells(i) match {
                    case 'm' => // another master: not dangerous, but an obstacle
                        nearestEnemyMaster = Some(cellRelPos)
                        if(distance < 3) 1000 else 0
                        
                    case 's' => // another slave: potentially dangerous?
                        100

                    case 'S' => // out own slave
                        0

                    case 'B' => // good beast: valuable, but runs away
                       // bot.log(dest.distanceTo(XY(currentX, currentY)).toString)
                        if (!dest.isDefined || src.distanceTo(dest) > src.distanceTo(XY(currentCOL, currentROW))) dest = dest.update(currentCOL, currentROW)
                        0

                    case 'P' => // good plant: less valuable, but does not run
                        //bot.log(dest.distanceTo(XY(currentX, currentY)).toString)
                        if (!dest.isDefined || src.distanceTo(dest) > src.distanceTo(XY(currentCOL, currentROW))) dest = dest.update(currentCOL, currentROW)
                        0

                    case 'b' => // bad beast: dangerous, but only if very close
                        if (distance < 3) 100 else 10

                    case 'p' => // bad plant: bad, but only if I step on it
                        if(distance < 2) 100 else 0

                    case 'W' => // wall: harmless, just don't walk into it
                        1000
                        

                    case _ => 0
                }
                cellDetails(currentCOL)(currentROW) = tempXY
            }
            
        }
        val openList = new ListBuffer[XY]()
        openList += src

        var foundDest = false; 
        
        while(!openList.isEmpty) {
            var p: XY = openList.head
            openList -= p
            
            var currentROW = p.y 
            var currentCOL = p.x 
            closedList(currentROW)(currentCOL) = true
            var gNew = 0.0
            var hNew = 0.0
            var fNew = 0.0
            if (XY.isValid(currentCOL-1, currentROW) == true)
            {
                if (dest.x == currentCOL-1 && dest.y == currentROW) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL-1)(currentROW).parentCOL = currentCOL
                    cellDetails(currentCOL-1)(currentROW).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL-1)(currentROW) == false && 
                     cellDetails(currentCOL-1)(currentROW).f < 1000) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.0
                    //hNew = XY.calculateHValue(currentCOL-1, currentROW, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL-1)(currentROW).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL-1)(currentROW).f = fNew 
                        cellDetails(currentCOL-1)(currentROW).g = gNew 
                        cellDetails(currentCOL-1)(currentROW).h = hNew
                        cellDetails(currentCOL-1)(currentROW).parentCOL = currentCOL 
                        cellDetails(currentCOL-1)(currentROW).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL-1)(currentROW)
                    } 
                } 
            }
            if (XY.isValid(currentCOL+1, currentROW) == true)
            {
                if (dest.x == currentCOL+1 && dest.y == currentROW) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL+1)(currentROW).parentCOL = currentCOL
                    cellDetails(currentCOL+1)(currentROW).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL+1)(currentROW) == false && 
                     cellDetails(currentCOL+1)(currentROW).f < 1000.0) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.00
                    //hNew = XY.calculateHValue(currentCOL+1, currentROW, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL+1)(currentROW).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL+1)(currentROW).f = fNew 
                        cellDetails(currentCOL+1)(currentROW).g = gNew 
                        cellDetails(currentCOL+1)(currentROW).h = hNew
                        cellDetails(currentCOL+1)(currentROW).parentCOL = currentCOL 
                        cellDetails(currentCOL+1)(currentROW).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL+1)(currentROW)
                    } 
                } 
            }
            
            if (XY.isValid(currentCOL, currentROW+1) == true)
            {
                if (dest.x == currentCOL && dest.y == currentROW+1) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL)(currentROW+1).parentCOL = currentCOL
                    cellDetails(currentCOL)(currentROW+1).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL)(currentROW+1) == false && 
                     cellDetails(currentCOL)(currentROW+1).f < 1000) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.00
                    //hNew = XY.calculateHValue(currentCOL, currentROW+1, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL)(currentROW+1).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL)(currentROW+1).f = fNew 
                        cellDetails(currentCOL)(currentROW+1).g = gNew 
                        cellDetails(currentCOL)(currentROW+1).h = hNew
                        cellDetails(currentCOL)(currentROW+1).parentCOL = currentCOL 
                        cellDetails(currentCOL)(currentROW+1).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL)(currentROW+1)
                    } 
                } 
            }
            
            if (XY.isValid(currentCOL, currentROW-1) == true)
            {
                if (dest.x == currentCOL && dest.y == currentROW-1) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL)(currentROW-1).parentCOL = currentCOL
                    cellDetails(currentCOL)(currentROW-1).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL)(currentROW-1) == false && 
                     cellDetails(currentCOL)(currentROW-1).f < 1000.0) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.00
                    //hNew = XY.calculateHValue(currentCOL, currentROW-1, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL)(currentROW-1).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL)(currentROW-1).f = fNew 
                        cellDetails(currentCOL)(currentROW-1).g = gNew 
                        cellDetails(currentCOL)(currentROW-1).h = hNew
                        cellDetails(currentCOL)(currentROW-1).parentCOL = currentCOL 
                        cellDetails(currentCOL)(currentROW-1).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL)(currentROW-1)
                    } 
                } 
            }
            if (XY.isValid(currentCOL-1, currentROW+1) == true)
            {
                if (dest.x == currentCOL-1 && dest.y == currentROW+1) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL-1)(currentROW+1).parentCOL = currentCOL
                    cellDetails(currentCOL-1)(currentROW+1).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL-1)(currentROW+1) == false && 
                     cellDetails(currentCOL-1)(currentROW+1).f < 1000) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.44
                    //hNew = XY.calculateHValue(currentCOL-1, currentROW+1, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL-1)(currentROW+1).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL-1)(currentROW+1).f = fNew 
                        cellDetails(currentCOL-1)(currentROW+1).g = gNew 
                        cellDetails(currentCOL-1)(currentROW+1).h = hNew
                        cellDetails(currentCOL-1)(currentROW+1).parentCOL = currentCOL 
                        cellDetails(currentCOL-1)(currentROW+1).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL-1)(currentROW+1)
                    } 
                } 
            }
            if (XY.isValid(currentCOL-1, currentROW-1) == true)
            {
                if (dest.x == currentCOL-1 && dest.y == currentROW-1) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL-1)(currentROW-1).parentCOL = currentCOL
                    cellDetails(currentCOL-1)(currentROW-1).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL-1)(currentROW-1) == false && 
                     cellDetails(currentCOL-1)(currentROW-1).f < 1000.0) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.44
                    //hNew = XY.calculateHValue(currentCOL-1, currentROW-1, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL-1)(currentROW-1).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL-1)(currentROW-1).f = fNew 
                        cellDetails(currentCOL-1)(currentROW-1).g = gNew 
                        cellDetails(currentCOL-1)(currentROW-1).h = hNew
                        cellDetails(currentCOL-1)(currentROW-1).parentCOL = currentCOL 
                        cellDetails(currentCOL-1)(currentROW-1).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL-1)(currentROW-1)
                    } 
                } 
            }
            if (XY.isValid(currentCOL+1, currentROW+1) == true)
            {
                if (dest.x == currentCOL+1 && dest.y == currentROW+1) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL+1)(currentROW+1).parentCOL = currentCOL
                    cellDetails(currentCOL+1)(currentROW+1).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL+1)(currentROW+1) == false && 
                     cellDetails(currentCOL+1)(currentROW+1).f < 1000.0) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.44
                    hNew = XY.calculateHValue(currentCOL+1, currentROW+1, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL+1)(currentROW+1).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL+1)(currentROW+1).f = fNew 
                        cellDetails(currentCOL+1)(currentROW+1).g = gNew 
                        cellDetails(currentCOL+1)(currentROW+1).h = hNew
                        cellDetails(currentCOL+1)(currentROW+1).parentCOL = currentCOL 
                        cellDetails(currentCOL+1)(currentROW+1).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL+1)(currentROW+1)
                    } 
                } 
            }
            if (XY.isValid(currentCOL+1, currentROW-1) == true)
            {
                if (dest.x == currentCOL+1 && dest.y == currentROW-1) 
                { 
                // Set the Parent of the destination cell 
                    cellDetails(currentCOL+1)(currentROW-1).parentCOL = currentCOL
                    cellDetails(currentCOL+1)(currentROW-1).parentROW = currentROW
                    bot.log("Found")
                    //tracePath (cellDetails, dest) 
                    foundDest = true
                    directionValue = XY.Up
                } 
                else if (closedList(currentCOL+1)(currentROW-1) == false && 
                     cellDetails(currentCOL+1)(currentROW-1).f < 1000) 
                { 
                    gNew = cellDetails(currentCOL)(currentROW).g + 1.44
                    //hNew = XY.calculateHValue(currentCOL+1, currentROW-1, dest)
                    fNew = gNew + hNew
      
                    if (cellDetails(currentCOL+1)(currentROW-1).f > fNew) 
                    { 
                        // Update the details of this cell 
                        cellDetails(currentCOL+1)(currentROW-1).f = fNew 
                        cellDetails(currentCOL+1)(currentROW-1).g = gNew 
                        cellDetails(currentCOL+1)(currentROW-1).h = hNew
                        cellDetails(currentCOL+1)(currentROW-1).parentCOL = currentCOL 
                        cellDetails(currentCOL+1)(currentROW-1).parentROW = currentROW 
                        
                        openList += cellDetails(currentCOL+1)(currentROW-1)
                    } 
                } 
            }
        }
        
        bot.log(src.distanceTo(dest).toString)
        bot.log(src.toString)
        bot.log(dest.toString)
        (directionValue, nearestEnemyMaster, nearestEnemySlave)
    }
}



// -------------------------------------------------------------------------------------------------
// Framework
// -------------------------------------------------------------------------------------------------

class ControlFunctionFactory {
    def create = (input: String) => {
        val (opcode, params) = CommandParser(input)
        opcode match {
            case "React" =>
                val bot = new BotImpl(params)
                if( bot.generation == 0 ) {
                    ControlFunction.forMaster(bot)
                } else {
                    ControlFunction.forSlave(bot)
                }
                bot.toString
            case _ => "" // OK
        }
    }
}


// -------------------------------------------------------------------------------------------------


trait Bot {
    // inputs
    def inputOrElse(key: String, fallback: String): String
    def inputAsIntOrElse(key: String, fallback: Int): Int
    def inputAsXYOrElse(keyPrefix: String, fallback: XY): XY
    def view: View
    def energy: Int
    def time: Int
    def generation: Int

    // outputs
    def move(delta: XY) : Bot
    def say(text: String) : Bot
    def status(text: String) : Bot
    def spawn(offset: XY, params: (String,Any)*) : Bot
    def set(params: (String,Any)*) : Bot
    def log(text: String) : Bot
}

trait MiniBot extends Bot {
    // inputs
    def offsetToMaster: XY

    // outputs
    def explode(blastRadius: Int) : Bot
}


case class BotImpl(inputParams: Map[String, String]) extends MiniBot {
    // input
    def inputOrElse(key: String, fallback: String) = inputParams.getOrElse(key, fallback)
    def inputAsIntOrElse(key: String, fallback: Int) = inputParams.get(key).map(_.toInt).getOrElse(fallback)
    def inputAsXYOrElse(key: String, fallback: XY) = inputParams.get(key).map(s => XY(s)).getOrElse(fallback)

    val view = View(inputParams("view"))
    val energy = inputParams("energy").toInt
    val time = inputParams("time").toInt
    val generation = inputParams("generation").toInt
    def offsetToMaster = inputAsXYOrElse("master", XY.Zero)


    // output

    private var stateParams = Map.empty[String,Any]     // holds "Set()" commands
    private var commands = ""                           // holds all other commands
    private var debugOutput = ""                        // holds all "Log()" output

    /** Appends a new command to the command string; returns 'this' for fluent API. */
    private def append(s: String) : Bot = { commands += (if(commands.isEmpty) s else "|" + s); this }

    /** Renders commands and stateParams into a control function return string. */
    override def toString = {
        var result = commands
        if(!stateParams.isEmpty) {
            if(!result.isEmpty) result += "|"
            result += stateParams.map(e => e._1 + "=" + e._2).mkString("Set(",",",")")
        }
        if(!debugOutput.isEmpty) {
            if(!result.isEmpty) result += "|"
            result += "Log(text=" + debugOutput + ")"
        }
        result
    }

    def log(text: String) = { debugOutput += text + "\n"; this }
    def move(direction: XY) = append("Move(direction=" + direction + ")")
    def say(text: String) = append("Say(text=" + text + ")")
    def status(text: String) = append("Status(text=" + text + ")")
    def explode(blastRadius: Int) = append("Explode(size=" + blastRadius + ")")
    def spawn(offset: XY, params: (String,Any)*) =
        append("Spawn(direction=" + offset +
            (if(params.isEmpty) "" else "," + params.map(e => e._1 + "=" + e._2).mkString(",")) +
            ")")
    def set(params: (String,Any)*) = { stateParams ++= params; this }
    def set(keyPrefix: String, xy: XY) = { stateParams ++= List(keyPrefix+"x" -> xy.x, keyPrefix+"y" -> xy.y); this }
}


// -------------------------------------------------------------------------------------------------


/** Utility methods for parsing strings containing a single command of the format
  * "Command(key=value,key=value,...)"
  */
object CommandParser {
    /** "Command(..)" => ("Command", Map( ("key" -> "value"), ("key" -> "value"), ..}) */
    def apply(command: String): (String, Map[String, String]) = {
        /** "key=value" => ("key","value") */
        def splitParameterIntoKeyValue(param: String): (String, String) = {
            val segments = param.split('=')
            (segments(0), if(segments.length>=2) segments(1) else "")
        }

        val segments = command.split('(')
        if( segments.length != 2 )
            throw new IllegalStateException("invalid command: " + command)
        val opcode = segments(0)
        val params = segments(1).dropRight(1).split(',')
        val keyValuePairs = params.map(splitParameterIntoKeyValue).toMap
        (opcode, keyValuePairs)
    }
}


// -------------------------------------------------------------------------------------------------


/** Utility class for managing 2D cell coordinates.
  * The coordinate (0,0) corresponds to the top-left corner of the arena on screen.
  * The direction (1,-1) points right and up.
  */
case class XY(x: Int, y: Int) {
    override def toString = x + ":" + y

    var parentROW = -1
    var parentCOL = -1
    var f = 10000.0
    var g = 10000.0
    var h = 10000.0
    
    def isNonZero = x != 0 || y != 0
    def isZero = x == 0 && y == 0
    def isNonNegative = x >= 0 && y >= 0
    def isDefined = x != -1 && y != -1

    def updateX(newX: Int) = XY(newX, y)
    def updateY(newY: Int) = XY(x, newY)
    def update(newX: Int, newY: Int) = XY(newX, newY)

    def addToX(dx: Int) = XY(x + dx, y)
    def addToY(dy: Int) = XY(x, y + dy)

    def +(pos: XY) = XY(x + pos.x, y + pos.y)
    def -(pos: XY) = XY(x - pos.x, y - pos.y)
    def *(factor: Double) = XY((x * factor).intValue, (y * factor).intValue)

    def distanceTo(pos: XY): Double = (this - pos).length // Phythagorean
    def length: Double = math.sqrt(x * x + y * y) // Phythagorean

    def stepsTo(pos: XY): Int = (this - pos).stepCount // steps to reach pos: max delta X or Y
    def stepCount: Int = x.abs.max(y.abs) // steps from (0,0) to get here: max X or Y

    def signum = XY(x.signum, y.signum)

    def negate = XY(-x, -y)
    def negateX = XY(-x, y)
    def negateY = XY(x, -y)
}


object XY {
    /** Parse an XY value from XY.toString format, e.g. "2:3". */
    def apply(s: String) : XY = { val a = s.split(':'); XY(a(0).toInt,a(1).toInt) }

    val Zero = XY(0, 0)
    val One = XY(1, 1)

    val Right     = XY( 1,  0)
    val RightUp   = XY( 1, -1)
    val Up        = XY( 0, -1)
    val UpLeft    = XY(-1, -1)
    val Left      = XY(-1,  0)
    val LeftDown  = XY(-1,  1)
    val Down      = XY( 0,  1)
    val DownRight = XY( 1,  1)

    def fromDirection45(index: Int): XY = index match {
        case Direction45.Right => Right
        case Direction45.RightUp => RightUp
        case Direction45.Up => Up
        case Direction45.UpLeft => UpLeft
        case Direction45.Left => Left
        case Direction45.LeftDown => LeftDown
        case Direction45.Down => Down
        case Direction45.DownRight => DownRight
    }
    
    def isValid (x: Int, y: Int): Boolean =
        (x >= 0) && (x < 31) && 
           (y >= 0) && (y < 31)
           
    def calculateHValue(row: Int, col: Int, dest: XY) 
    { 
        // Return using the distance formula 
        math.sqrt((row-dest.x)*(row-dest.x) 
                              + (col-dest.y)*(col-dest.y))
    }

    def fromDirection90(index: Int): XY = index match {
        case Direction90.Right => Right
        case Direction90.Up => Up
        case Direction90.Left => Left
        case Direction90.Down => Down
    }

    def apply(array: Array[Int]): XY = XY(array(0), array(1))
}


object Direction45 {
    val Right = 0
    val RightUp = 1
    val Up = 2
    val UpLeft = 3
    val Left = 4
    val LeftDown = 5
    val Down = 6
    val DownRight = 7
}


object Direction90 {
    val Right = 0
    val Up = 1
    val Left = 2
    val Down = 3
}


// -------------------------------------------------------------------------------------------------


case class View(cells: String) {
    val size = math.sqrt(cells.length).toInt
    val center = XY(size / 2, size / 2)

    def apply(relPos: XY) = cellAtRelPos(relPos)

    def indexFromAbsPos(absPos: XY) = absPos.x + absPos.y * size
    def absPosFromIndex(index: Int) = XY(index % size, index / size)
    def absPosFromRelPos(relPos: XY) = relPos + center
    def cellAtAbsPos(absPos: XY) = cells.charAt(indexFromAbsPos(absPos))

    def indexFromRelPos(relPos: XY) = indexFromAbsPos(absPosFromRelPos(relPos))
    def relPosFromAbsPos(absPos: XY) = absPos - center
    def relPosFromIndex(index: Int) = relPosFromAbsPos(absPosFromIndex(index))
    def cellAtRelPos(relPos: XY) = cells.charAt(indexFromRelPos(relPos))

    def offsetToNearest(c: Char) = {
        val matchingXY = cells.view.zipWithIndex.filter(_._1 == c)
        if( matchingXY.isEmpty )
            None
        else {
            val nearest = matchingXY.map(p => relPosFromIndex(p._2)).minBy(_.length)
            Some(nearest)
        }
    }
}

