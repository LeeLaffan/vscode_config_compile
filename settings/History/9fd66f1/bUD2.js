function calculateDaysBetweenDates(begin, end) {
  return Math.floor((end - begin) / (1000 * 60 * 60 * 24));
}