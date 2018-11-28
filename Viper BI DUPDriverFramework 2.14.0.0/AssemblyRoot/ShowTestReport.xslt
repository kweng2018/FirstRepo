<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:template match="/">
		<HTML>
			<xsl:for-each select="TestResults">
			<TITLE>
				<xsl:value-of select="ProcessStep" />
			</TITLE>
			<!--<ProcessStep>Final_Test</ProcessStep>
			<AssemblyType_Rev>DVBH_Sciliar</AssemblyType_Rev>
			<SoftwareRef>DVBH_Test_Software_Rev1.0.9</SoftwareRef>
			<FWVersion>dvbsbApRel09.bin</FWVersion>
			<TestDate>22_11_2007_15_13_17</TestDate>
			<Status>FAIL</Status>
			<SerialNumber>08240093</SerialNumber>
			<Testset>AFAIPC0117WXP</Testset>-->
			<FONT SIZE="3" COLOR="black">
				<TABLE BORDER="0">
					<TR>
						<TD>
							<B>Sequence Phase: </B>
						</TD>
						<TD>
							<B>
								<xsl:value-of select="ProcessStep" />
							</B>
						</TD>
					</TR>
					<TR>
						<TD>
							<B>Test Date:</B>
						</TD>
						<TD>
							<B>
								<xsl:value-of select="TestDate" />
							</B>
						</TD>
					</TR>
					<TR>
						<TD>
							<B>Test SW Version:</B>
						</TD>
						<TD>
							<B>
								<xsl:value-of select="Test_SW_Rev" />
							</B>
						</TD>
					</TR>
					<TR>
					<TD>
						<B>HW Rev:</B>
					</TD>
					<TD>
						<B>
							<xsl:value-of select="HW_Rev" />
						</B>
					</TD>
				</TR>
				<TR>
					<TD>
						<B>FW Rev:</B>
					</TD>
					<TD>
						<B>
							<xsl:value-of select="FW_Rev" />
						</B>
					</TD>
				</TR>
				<TR>
					<TD>
						<B>Serial Number:</B>
					</TD>
					<TD>
						<B>
							<xsl:value-of select="Serial_Number" />
						</B>
					</TD>
				</TR>
				<TR>
					<TD>
						<B>Test Station:</B>
					</TD>
					<TD>
						<B>
							<xsl:value-of select="Testset" />
						</B>
					</TD>
				</TR>
				<TR>
					<TD>
						<B>Test Time (sec):</B>
					</TD>
					<TD>
						<B>
							<xsl:value-of select="TestTime" />
						</B>
					</TD>
				</TR>
				<TR>
					<TD>
						<B>Status:</B>
					</TD>
					<TD>
						<B>
							<xsl:value-of select="Status" />
						</B>
					</TD>
				</TR>
					<!--Assembly Type: <xsl:value-of select="AssemblyType_Rev" /><BR></BR>
					Test SW Version: <xsl:value-of select="SoftwareRef" /><BR></BR>
					FW Version: <xsl:value-of select="SoftwareRef" /><BR></BR>
					Test Date: <xsl:value-of select="TestDate" /><BR></BR>
					Serial Number: <xsl:value-of select="SerialNumber" /><BR></BR>
					Test Station: <xsl:value-of select="Testset" /><BR></BR>
					Status:	Status:<BR></BR>-->
			
			<BR></BR>
		</TABLE>
			</FONT>
			<BR></BR>
			</xsl:for-each>
			<BODY>
				<xsl:for-each select="TestResults/TestGroup">
					<TABLE BORDER="0">
						<TR>
							<FONT SIZE="3" COLOR="black">
							<TD>
								<B>
									TEST CASE:
								</B>
							</TD>
								<TD>
								<B>
								<xsl:if test="@status = 'PASS'">
									<xsl:attribute name="style">
										<xsl:text>color:green</xsl:text>
									</xsl:attribute>
								</xsl:if>
								<xsl:if test="@status = 'FAIL'">
									<xsl:attribute name="style">
										<xsl:text>color:red</xsl:text>
									</xsl:attribute>
								</xsl:if>
								<xsl:value-of select="@name" />
								</B>
								</TD>
							</FONT>
						</TR>
					</TABLE>
					<TABLE BORDER="1">
						<TR>
							<TD>
								<B>Test Name</B>
							</TD>
							<TD>
								<B>LL</B>
							</TD>
							<TD>
								<B>Result</B>
							</TD>
							<TD>
								<B>UL</B>
							</TD>
							<TD>
								<B>Unit</B>
							</TD>
							<TD>
								<B>Status</B>
							</TD>
						</TR>
						<xsl:for-each select="Test">
							<TR>
								<TD>
									<xsl:value-of select="@name" />
								</TD>
								<TD>
									<xsl:value-of select="@LL" />
									<xsl:if test="@unit = 'bool'">
										NaN
									</xsl:if>
								</TD>
								<TD>
									<xsl:if test="@status = 'PASS'">
										<xsl:attribute name="style">
											<xsl:text>color:green</xsl:text>
										</xsl:attribute>
									</xsl:if>
									<xsl:if test="@status = 'FAIL'">
										<xsl:attribute name="style">
											<xsl:text>color:red</xsl:text>
										</xsl:attribute>
									</xsl:if>
									<xsl:value-of select="Result" />
								</TD>
								<TD>
									<xsl:value-of select="@UL" />
									<xsl:if test="@unit = 'bool'">
										NaN
									</xsl:if>
								</TD>
								<TD>
									<xsl:value-of select="@unit" />
								</TD>
								<!-- highlight negative growth in red -->
								<!--<xsl:if test="@author = 'jm'">
								Author equals "jm"
							</xsl:if>-->
								<TD>
									<xsl:if test="@status = 'PASS'">
										<xsl:attribute name="style">
											<xsl:text>color:green</xsl:text>
										</xsl:attribute>
									</xsl:if>
									<xsl:if test="@status = 'FAIL'">
										<xsl:attribute name="style">
											<xsl:text>color:red</xsl:text>
										</xsl:attribute>
									</xsl:if>
									<xsl:value-of select="@status" />
								</TD>
							</TR>
						</xsl:for-each>
					</TABLE>
					<BR></BR>
				</xsl:for-each>
			</BODY>
		</HTML>
	</xsl:template>
</xsl:stylesheet>