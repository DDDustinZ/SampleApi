do {
  sleep 1
  sqlcmd -S $args[0],$args[1] -U $args[2] -P $args[3] -Q " "
} until($?)
