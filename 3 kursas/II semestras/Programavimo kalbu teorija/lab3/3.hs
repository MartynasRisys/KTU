import System.IO
import Control.Monad

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

f :: [String] -> [Float]
f = map read

getMedian :: [Float] -> Float -> Int
getMedian list len = do
    if (len `mod` 2 == 0)
        then (list!!(round (len `div` 2 - 1)) + list!!(round (len / 2)))
    else 0
