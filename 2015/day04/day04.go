package day04

import (
	"crypto/md5"
	"embed"
	"encoding/hex"
	"strconv"
	"strings"
)

//go:embed input.txt
var content embed.FS

// Run day three
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	strInput := string(input)

	index := 1
	part1 := 0
	part2 := 0
	for true { // for is while
		hasher := md5.New()
		hasher.Write([]byte(strInput + strconv.Itoa(index)))
		hexStr := hex.EncodeToString(hasher.Sum(nil))

		if part1 == 0 && strings.HasPrefix(hexStr, "00000") {
			part1 = index
		}

		if strings.HasPrefix(hexStr, "000000") {
			part2 = index
			break
		}

		index++
	}

	return part1, part2
}
