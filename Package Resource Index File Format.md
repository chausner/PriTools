# Package Resource Index File Format

> [!NOTE]
> The information in this document has been collected through analysis of files and trial-and-error, and is by no means guaranteed to be correct, complete, or up-to-date. Use at your own risk!

## Conventions and Data Types

The following abbreviations for data types are used:

Abbreviation | Description
-------------|------------
byte         | unsigned 8-bit integer
uint16       | unsigned 16-bit integer
uint32       | unsigned 32-bit integer
char         | 8-bit ASCII character
wchar        | UTF-16LE Unicode character
wcharz       | zero-terminated UTF-16LE Unicode string
char[16]     | ASCII string with fixed length

Unless noted otherwise, indices are zero-based. Bits are numbered from zero onwards starting with the least-significant bit.

## File Structure

The overall file structure of Package Resource Index files consists of a header, a table of contents, a number of sections and a footer. The table of contents, every section and the footer must be aligned to a multiple of 8 bytes. Zero-padding bytes are inserted where necessary.

> Header  
> Table of contents  
> Section 1  
> Section 2  
> …  
> Section n  
> Footer

### Header

Offset | Data Type | Description
------ | --------- | -----------
0      | char[8]   | version identifier
8      | uint16    | unknown, zero
10     | uint16    | unknown, one
12     | uint32    | total file size
16     | uint32    | offset of table of contents
20     | uint32    | offset of first section
24     | uint16    | number of sections
26     | uint16    | unknown, 0xFFFF
28     | uint32    | unknown, zero

> - **version identifier**: identifies the file as a Package Resource Index and encodes file version information. Common values are
>   - "mrm_pri0": client 6.2.1 (Windows 8)
>   - "mrm_pri1": client 6.3.0 (Windows 8.1)
>   - "mrm_prif": Windows Phone 6.3.1
>   - "mrm_pri2": Universal 10.0.0 (Windows 10)
>   - "mrm_pri3": Universal 10.0.0.5

### Table of Contents

The table of contents provides information about each section in the file. For each section, there is exactly one corresponding entry in the table of contents. Each entry has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | char[16]  | section identifier
16     | uint16    | flags
18     | uint16    | section flags
20     | uint32    | section qualifier
24     | uint32    | section offset, relative to offset of first section
28     | uint32    | section length

### Sections

Each section consists of a section header, the actual section data and a section footer. If the length of the section data is not a multiple of 8 bytes, appropriate padding is inserted before the section footer. The padding is not counted in any section length fields.

The structure of each section header is as follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | char[16]  | section identifier
16     | uint32    | section qualifier
20     | uint16    | flags
22     | uint16    | section flags
24     | uint32    | section length
28     | uint32    | unknown, zero

The structure of each section footer is:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint32    | 0xF5DEDEFA
4      | uint32    | section length, as in section header

#### PRI Descriptor Section

The PRI Descriptor Section has the section identifier "[mrm_pridescex]\0" and provides basic information about other sections in the file.

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | flags
2      | uint16    | section index of Included File List Section, 0xFFFF if not present
4      | uint16    | unknown, zero
6      | uint16    | number of Hierarchical Schema Sections in the file
8      | uint16    | number of Decision Info Sections in the file
10     | uint16    | number of Resource Map Sections in the file
12     | uint16    | section index of primary resource map, 0xFFFF if not present
14     | uint16    | number of Referenced File Sections in the file
16     | uint16    | number of Data Item Sections in the file
18     | uint16    | unknown, zero

> - **flags**:
>   - bit 0: AutoMerge
>   - bit 1: IsDeploymentMergeable
>   - bit 2: IsDeploymentMergeResult
>   - bit 3: IsAutomergeMergeResult

The section indices of all Hierarchical Schema Sections, Decision Info Sections, Resource Map Sections, Referenced File Sections and Data Item Sections follow in this exact order as uint16s.

#### Hierarchical Schema Section

The Hierarchical Schema Section exists in two versions, a compact one with the section identifier "[mrm_hschema]  \0" and an extended one with the section identifier "[mrm_hschemaex] ". The hierarchical schema consists of a file system-like structure of scopes and items, the items being the logical resources and the scopes describing a directory tree in which the items are organized.

In some files, this section is empty and does not contain any data apart from the common section header and footer.

Offset | Data Type            | Description
------ | -------------------- | -----------
0      | uint16               | unknown, one
2      | uint16               | length of unique name of resource map in characters, null-terminator included
4      | uint16               | length of name of resource map in characters, null-terminator included
6      | uint16               | unknown, zero
8      | char[16]             | hname identifier
24     | HSCHEMA_VERSION_INFO | hierarchical schema version info
44     | wcharz               | unique name of resource map
44 + ? | wcharz               | name of resource map
44 + ? | uint16               | unknown, zero
46 + ? | uint16               | length of longest full path of all resource names
48 + ? | uint16               | unknown, zero
50 + ? | uint32               | number of resource names, usually number of scopes + items
54 + ? | uint32               | number of scopes
58 + ? | uint32               | number of items
62 + ? | uint32               | length of Unicode name block
66 + ? | uint32               | unknown
70 + ? | uint32               | unknown

> - **hname identifier**: only present in the extended Hierarchical Schema Section. Observed values are "[def_hnames]   \0" and "[def_hnamesx]  \0".
> - **unknown at 70 + ?**: only present in the extended Hierarchical Schema Section and if hname identifier is "[def_hnamesx]  \0".

HSCHEMA_VERSION_INFO has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | major version
2      | uint16    | minor version
4      | uint32    | unknown, zero
8      | uint32    | checksum
12     | uint32    | number of scopes
16     | uint32    | number of items

> - **checksum**: a CRC32-based checksum computed on the unique name, the name, the section indices of the Resource Map Section and Data Item Section, and the names of all scopes and items

Names of scopes and items are stored in two blocks, a Unicode name block and an ASCII name block, depending on whether the name can be represented in ASCII or not.

> [!NOTE]
> In the following, any use of the term "index" will refer to the indices of scopes and items as they appear in the following table, while the term "index property" will refer to the property of the same name that each scope and item has.

For each resource name, i.e. scope or item, follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | parent scope index
2      | uint16    | length of full path
4      | wchar     | uppercase first character of name, '\0' if name is empty
6      | byte      | length of name in characters, null-terminator excluded, 0 if the length is bigger than 255
7      | byte      | flags and upper bits of name offset
8      | uint16    | offset of name in ASCII or Unicode name block in characters
10     | uint16    | index property

> - **flags and upper bits of name offset**:
>   - bits 0-3: upper bits 16-19 of name offset  
      bit 4: set if resource name is a scope, unset if it is an item  
      bit 5: set if name is stored in the ASCII name block, unset if it is stored in the Unicode name block
     
For each scope, sorted by the index property, follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | scope index
2      | uint16    | child count
4      | uint16    | scope or item index of first child, all other children follow sequentially
6      | uint16    | unknown, zero

For each item, sorted by the index property, follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | item index

The Unicode name block and the ASCII name block follow.

#### Decision Info Section

The Decision Info Section has the section identifier "[mrm_decn_info]\0" and stores all resource qualifiers. Qualifiers that appear together for resource candidates (e.g. a candidate may have the qualifiers lang=de-DE and scale=100) are grouped into qualifier sets. A qualifier can be part of more than one qualifier set. Qualifier sets that are specified for different candidates of the same resource item (e.g. a resource item may have candidates for lang=de-DE;scale=100 and lang=en-US;scale=100) are grouped into decisions. A qualifier set can be part of more than one decision. Qualifiers and qualifier sets are referenced through their indices which are stored in a separate index table.

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | number of distinct qualifiers
2      | uint16    | number of qualifiers
4      | uint16    | number of qualifier sets
6      | uint16    | number of decisions
8      | uint16    | number of entries in the index table
10     | uint16    | length of qualifier value block in characters

For each decision follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | index of the first qualifier set index in the index table
2      | uint16    | number of qualifiers sets in decision

For each qualifier set follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | index of the first qualifier index in the index table
2      | uint16    | number of qualifiers in qualifier set

For each qualifier follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | index of distinct qualifier
2      | uint16    | priority
4      | uint16    | fallback score, values range from 0 to 1000
6      | uint16    | unknown, zero

For each distinct qualifier follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | unknown
2      | uint16    | qualifier type
4      | uint16    | unknown
6      | uint16    | unknown
8      | uint16    | offset of qualifier value in qualifier value block, in characters

> - **qualifier type**: valid types are
>   - Language (0)
>   - Contrast (1)
>   - Scale (2)
>   - HomeRegion (3)
>   - TargetSize (4)
>   - LayoutDirection (5)
>   - Theme (6)
>   - AlternateForm (7)
>   - DXFeatureLevel (8)
>   - Configuration (9)
>   - DeviceFamily (10)
>   - Custom (11)

The index table follows as an array of uint16s, then the qualifier value block follows. Qualifier values are stored as zero-terminated Unicode strings.

#### Resource Map Section

The Resource Map Section exists in two versions with the section identifiers "[mrm_res_map__]\0" and "[mrm_res_map2_]\0". It assigns to each resource item a decision and a set of candidates. It may contain embedded resource data directly in the section or reference data items in a Data Item Section.

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | length of environment references block
2      | uint16    | number of references in environment references block
4      | uint16    | section index of Hierarchical Schema Section
6      | uint16    | length of hierarchical schema reference block
8      | uint16    | section index of Decision Info Section
10     | uint16    | number of entries in resource value type table
12     | uint16    | number of entries in item to iteminfo group table
14     | uint16    | number of entries in iteminfo group table
16     | uint32    | number of entries in iteminfo table
20     | uint32    | number of candidates
24     | uint32    | length of embedded data block
28     | uint32    | length of table extension block

The block of environment references follows. It consists of a sequence of environment references, each 0x22C bytes long. The detailed structure of these is unknown. The number of environment references this block contains must be non-zero for section version 1 ("[mrm_res_map__]\0"), and zero for section version 2 ("[mrm_res_map2_]\0").

The hierarchical schema reference block follows. It has the following structure:

Offset | Data Type            | Description
------ | -------------------- | -----------
0      | HSCHEMA_VERSION_INFO | hierarchical schema version info
20     | uint16               | length of unique id in characters, null-terminator included
22     | uint16               | unknown, zero
24     | uint32               | unknown
28     | uint32               | unknown
32     | wcharz               | unique id

The resource value type table follows. Each entry has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint32    | unknown, 4
4      | uint32    | resource value type

> - **resource value type**: valid values are
>   - String (0)
>   - Path (1)
>   - EmbeddedData (2)
>   - AsciiString (3)
>   - Utf8String (4)
>   - AsciiPath (5)
>   - Utf8Path (6)

PRI file version "mrm_pri0" appears to have entries for the first three resource value types only. Later versions have entries for all seven types.

The item to iteminfo group table follows. Each entry has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | index property of first resource item
2      | uint16    | index of iteminfo group

> - **index of iteminfo group**: if this value is greater than or equal to the number of item info groups, it encodes a group containing only one iteminfo, namely the iteminfo at index (value - [number of item info groups])

Only the index property of the first resource item is stored. All other resource items have consecutively increasing index properties. The number of resource items is determined by the number of iteminfos in the associated iteminfo group.

The iteminfo group table follows. Each entry has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | number of iteminfos in this group
2      | uint16    | index of the first iteminfo in this group

Only the index of the first iteminfo is stored. All other iteminfos have consecutively increasing indices.

The iteminfo table follows. Each entry maps a resource name to a decision and a set of candidates. The index that an entry appears in this table corresponds to the index property of the resource name it is associated with. Each entry has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | index of decision
2      | uint16    | index of first candidate

Only the index of the first candidate is stored. All other candidates have consecutively increasing indices. The number of candidates is determined by the number of qualifier sets in the decision.

The table extension block follows. It allows storing entries of the three preceding tables with uint32 data types which would exceed the size of the uint16 data type. The block has the following structure:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint32    | number of additional entries of the item to iteminfo group table
2      | uint32    | number of additional entries of the item info group table
4      | uint32    | number of additional entries of the iteminfo table

Entries for the three tables then follow in the same format as above with the same semantics but using uint32 data types instead of uint16 data types. These entries are to be appended to the respective tables.

For each candidate follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | byte      | candidate type, zero or one

Depending on the candidate type, the resource data is either stored in the embedded data block at the end of this section, or in a Data Item Section in the same or external PRI file.

If the candidate type is zero, the data that follows for the candidate is:

Offset | Data Type | Description
------ | --------- | -----------
1      | byte      | resource value type, specified as an index into the resource value type table
2      | uint16    | embedded data length
4      | uint32    | offset of the embedded data in the embedded data block

If the candidate type is one, the data that follows for the candidate is:

Offset | Data Type | Description
------ | --------- | -----------
1      | byte      | resource value type, specified as an index into the resource value type table
2      | uint16    | source file
4      | uint16    | index of the data item storing the data
6      | uint16    | section index of the Data Item Section storing the data

> - **source file**: zero if the specified Data Item Section is located in the same file, else 1 + the index of a file path in a Referenced File Section that points to an external PRI file which contains the data

Candidates of type zero seem to only appear in PRI file version "mrm_pri0".

The embedded data block follows.

#### Data Item Section

The Data Item Section has the section identifier "[mrm_dataitem] \0" and stores resource data.

Offset | Data Type | Description
------ | --------- | -----------
0      | uint32    | unknown, zero
4      | uint16    | number of stored strings
6      | uint16    | number of stored blobs
8      | uint32    | total length of stored data

Data items are either strings (for short textual data) or blobs (for binary data). Data items are referenced from other sections by their index which is defined by the order in which they appear in the tables below (strings have lower indices than blobs).

For each stored string follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | string offset, relative to start of stored data
2      | uint16    | string length in bytes

For each stored blob follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint32    | blob offset, relative to start of stored data
4      | uint32    | blob length in bytes

All stored data follows.

#### Referenced File Section

The Referenced File Section has the section identifier "[def_file_list]\0" and holds a tree of file and folder paths which are referenced from other sections.

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | number of roots
2      | uint16    | number of folders
4      | uint16    | number of files
6      | uint16    | unknown, zero
8      | uint32    | length of Unicode name block in characters

For each folder follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | unknown, zero
2      | uint16    | index of parent folder, 0xFFFF if no parent exists (root)
4      | uint16    | number of folders in this folder
6      | uint16    | index of first folder in this folder
8      | uint16    | number of files in this folder
10     | uint16    | index of first file in this folder
12     | uint16    | length of folder name in characters
14     | uint16    | length of full folder path
16     | uint32    | offset of folder name in Unicode name block

For each file follows:

Offset | Data Type | Description
------ | --------- | -----------
0      | uint16    | unknown
2      | uint16    | index of parent folder
4      | uint16    | length of full file path
6      | uint16    | length of file name in characters
8      | uint32    | offset of file name in Unicode name block

The block of Unicode names follows.

## Footer

Offset | Data Type | Description
------ | --------- | -----------
0      | uint32    | 0xFFDEDEFA
4      | uint32    | total file size, as in header
8      | char[8]   | version identifier, as in header
