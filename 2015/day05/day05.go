package day05

import (
	"embed"
	"strings"
)

//go:embed input.txt
var content embed.FS

// Run day five
func Run() (int, int) {
	input, _ := content.ReadFile("input.txt")
	lines := strings.Split(string(input), "\n")

	part1 := PartOne(lines)
	part2 := PartTwo(lines)

	return part1, part2
}

func PartOne(lines []string) int {
	var nbrNice = 0
	var vowels = map[string]bool{"a": true, "e": true, "i": true, "o": true, "u": true}
	var forbidden = map[string]bool{"ab": true, "cd": true, "pq": true, "xy": true}

	for _, line := range lines {
		var numberOfVowels = 0
		var duplicatedSegment = false
		var naughty = false
		for i := 0; i < len(line)-1; i++ {
			ch := string(line[i])
			segment := ch + string(line[i+1])

			if _, ok := forbidden[segment]; ok {
				naughty = true
				break
			}

			if _, ok := vowels[ch]; ok {
				numberOfVowels++
			}

			if segment[0] == segment[1] {
				duplicatedSegment = true
			}
		}

		if !naughty && duplicatedSegment && numberOfVowels > 2 {
			nbrNice++
		}
	}

	return nbrNice
}

func PartTwo(lines []string) int {
	var nbrNice = 0
	for _, line := range lines {
		line = line[0:16]

		var oneInBetween = false
		var duplicatedSegment = false
		var segments = map[string]int{}

		for i := 0; i < len(line)-1; i++ {
			ch1 := string(line[i])
			ch2 := string(line[i+1])
			ch3 := ""

			if i < len(line)-2 {
				ch3 = string(line[i+2])
			}

			segment := ch1 + ch2
			segments[segment]++

			if ch1 == ch3 {
				oneInBetween = true
			}
		}

		for element := range segments {
			if segments[element] > 1 {
				var sub = strings.Replace(line, element, "", 1)
				if strings.Contains(sub, element) {
					duplicatedSegment = true
				}
			}
		}

		if oneInBetween && duplicatedSegment {
			nbrNice++
		}
	}

	return nbrNice
}
