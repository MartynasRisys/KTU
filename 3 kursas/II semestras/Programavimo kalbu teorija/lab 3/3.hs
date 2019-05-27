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
        let listLength = length list
        let a = getMedian list listLength
        hClose handle
        

f :: [String] -> [Int]
f = map read

getMedian :: [Int] -> Int -> Float
getMedian list len = do
    if (len `mod` 2 == 0)
        then divideIntToFloat (list!!(len `div` 2 - 1) + list!!(len `div` 2)) 2
    else divideIntToFloat (list!!(len - 1)) 2

divideIntToFloat :: Int -> Int -> Float
divideIntToFloat a b = (fromIntegral a) / (fromIntegral b)