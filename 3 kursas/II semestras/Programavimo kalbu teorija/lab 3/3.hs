import System.IO
import Control.Monad
import Data.Function

main = do
        let list = []
        handle <- openFile "input.txt" ReadMode
        contents <- hGetContents handle
        let singlewords = words contents
            list = f singlewords
        print list
        writeFile "output.txt" ""
        iterateAllList list 1
        hClose handle
        

f :: [String] -> [Int]
f = map read

getMedian :: [Int] -> Int -> Float
getMedian list len = do
    if (len `mod` 2 == 0)
        then divideIntToFloat (list!!(len `div` 2 - 1) + list!!(len `div` 2)) 2
    else divideIntToFloat (list!!((len - 1) `div` 2)) 1

divideIntToFloat :: Int -> Int -> Float
divideIntToFloat a b = (fromIntegral a) / (fromIntegral b)

iterateAllList list currentIteration = do
    let lengthList = length list
    if (currentIteration <= lengthList)
        then do
            let median = getMedian list currentIteration
            let roundedMedian = floor median
            appendFile "output.txt" (show roundedMedian)
            appendFile "output.txt" ("\n")
            iterateAllList list (currentIteration + 1)
    else return ()

